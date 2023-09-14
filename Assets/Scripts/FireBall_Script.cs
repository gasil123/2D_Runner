using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Script : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private int damage;
 
    private Animator animator;
    private BoxCollider2D boxcollider2d;
    private bool hit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxcollider2d = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = ProjectileSpeed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxcollider2d.enabled = false;
        animator.SetTrigger("FireballHit");
        var enemyhealth = collision.gameObject.GetComponent<Enemy>();
        if (enemyhealth != null)
        {
            enemyhealth.TakeDamage(damage);
        }
    }
    public void Setdirection(Vector3 forward)
    {
        gameObject.SetActive(true);
        hit = false;
        boxcollider2d.enabled = true;
        transform.forward = forward;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false); 
    }
}
