using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class PlayerCollitionDetection : MonoBehaviour
{
    [SerializeField] private AudioSource coincollectSound; 
    [SerializeField] private AudioSource playerHurtSound; 
    [SerializeField] private AudioSource playerDeathSound;
    [SerializeField] private AudioSource playerHeal;
    [SerializeField] private AudioSource playerBulletCollect;

    AudioManager_Script AudioManager_;

    Animator animator;
    Player_Movement pm;
    [SerializeField] private GameObject GameoverUi;
    [SerializeField] private GameObject AudioManager;
    public GameObject[] hearts;
    public TextMeshProUGUI scoreText;
    int score = 0;
    [Tooltip("Count is the number of hearts -1")]
    [SerializeField] private int count = 2;
    [SerializeField] private int playerhealth = 3;
    private void Start()
    {
        animator = GetComponent<Animator>();
        pm = GetComponent<Player_Movement>();
        AudioManager_ = AudioManager.GetComponent<AudioManager_Script>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHurtSound.Play();
            animator.SetTrigger("playerHurt");
            if (count >= 0)
            {
                hearts[count].SetActive(false);
            }
            count--;
            playerhealth--;
            if (playerhealth <= 0)
            {
              StartCoroutine(PlayerDie());
            }
        }
    }
    IEnumerator PlayerDie()
    {
        animator.SetBool("DeathAnimation", true);
        playerDeathSound.Play();
        pm.enabled = false;
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        GameOver();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        var coins_Script = collision.gameObject.GetComponent<Coin_Script>();
        if (coins_Script != null)
        {
            coincollectSound.Play();
            score++;
            scoreText.text = "X"+ score.ToString();
            coins_Script.isalreadycollected = true;
            Destroy(collision.gameObject);
        }

        var fireball = collision.gameObject.GetComponent<fireBall_Collectable>();
        if (fireball != null)
        {
            gameObject.GetComponent<PlayerAttack>().NumberOfFireBullets += 1;
            playerBulletCollect.Play();
            Destroy(collision.gameObject);
        }

        var healthpotion = collision.gameObject.GetComponent<HealthPortion_collectable>();
        if (healthpotion != null && playerhealth < 3)
        {
            playerHeal.Play();
            playerhealth++;
            count++;
            hearts[count].SetActive(true);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("GameOverCollider"))
        {
            StartCoroutine(PlayerDie());
        }
    }
    void GameOver()
    {
        AudioManager_.Mutemaster();
        Time.timeScale = 0;
        GameoverUi.SetActive(true);
    }
}
