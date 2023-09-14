using System.Collections;
using UnityEngine;
using TMPro;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AudioSource PlayerAttackAudio;
    [SerializeField] private AudioSource PlayerShootAudio;
    [SerializeField] private float Firingcooldowntime;
    [SerializeField] private Transform attackpoint;
    [SerializeField] private Transform firingpoint;
    [SerializeField] private GameObject[] FireBalls;
    [SerializeField] private LayerMask enemylayers;
    [SerializeField] private float attackrange = 0.5f;
    [SerializeField] private TextMeshProUGUI fireballcount;
    [SerializeField] private int damage;
    public int NumberOfFireBullets = 5;

    private Animator animator;
    private float cooldowntimer;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        fireballcount.text = NumberOfFireBullets.ToString();
        if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown("SwordAttack"))
        {
            StartCoroutine("Attack");
        }
        bool canfire = cooldowntimer > Firingcooldowntime && NumberOfFireBullets > 0;
        if (Input.GetKeyDown(KeyCode.E) && canfire || SimpleInput.GetButtonDown("Firebutton") && canfire)
        {
            cooldowntimer = 0;
            StartCoroutine("Shootfireball");
        }
        cooldowntimer += Time.deltaTime;
    }
    IEnumerator Attack()
    {
        PlayerAttackAudio.Play();
        animator.SetTrigger("IsAttacking");
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, enemylayers);
        yield return new WaitForSeconds(0.2f);
        foreach(Collider2D enemy in HitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    IEnumerator Shootfireball()
    {
        PlayerShootAudio.Play();
        NumberOfFireBullets--;
        animator.SetTrigger("ShootFireball");
        yield return new WaitForSeconds(0.4f);
        FireBalls[FindfireBall()].transform.position = firingpoint.position;
        FireBalls[FindfireBall()].GetComponent<FireBall_Script>().Setdirection(transform.forward);
    }
    private void OnDrawGizmosSelected()
    {
        if(attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere (attackpoint.position, attackrange);
    }
    private int FindfireBall()
    {
        for(int i=0;i< FireBalls.Length; i++)
        {
            if (!FireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
