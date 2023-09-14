using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_WonTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource LevelCompleteSound;
    [SerializeField] private AudioSource portalinSound;
    [SerializeField] private float animationduration;
    [SerializeField] private GameObject levelWonUI;
    [SerializeField] private GameObject ControlUI;
   // public Transform target;
    GameObject player;
    Animator anim;
    Rigidbody2D rb;
    Player_Movement pm;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        pm = player.GetComponent<Player_Movement>();
        rb.simulated = true;
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;

            if (currentLevel >= PlayerPrefs.GetInt("levelunlocked"))
            {
                PlayerPrefs.SetInt("levelunlocked", currentLevel+1);
            }
            StartCoroutine(PortalIn());
        }
    }

    IEnumerator  PortalIn()
    {
        ControlUI.SetActive(false);
        pm.enabled = false;
        rb.simulated = false;
        anim.Play("EnteringDoor");
        StartCoroutine(EnterPortal());
        yield return new WaitForSeconds(0.7f);
        LevelCompleteSound.Play();
        levelWonUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    IEnumerator EnterPortal()
    {
        float timer = 0;
        portalinSound.Play();
        while(timer < 0.5)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, animationduration * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
