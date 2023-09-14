using UnityEngine;
using System.Collections;
using UnityEngine.Splines;
public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioSource EnemyHurtSound;
    [SerializeField] private AudioSource EnemyDeathSound;
    [SerializeField] private int maxHealth = 100;
    int currentHealth;
    Animator animator;
    
    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        EnemyHurtSound.Play();
        currentHealth -= damage;
        //play hurt animation
        animator.SetTrigger("BatHurt");
        if (currentHealth <= 0)
        {
           StartCoroutine( Die());
        }
    }

    IEnumerator Die()
    {
        // Die animation
        EnemyDeathSound.Play();
        animator.SetBool("BatDie", true);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SplineAnimate>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
