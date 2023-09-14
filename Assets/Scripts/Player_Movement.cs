using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float movementspeed;
    [SerializeField] private float jumpforce;
    [SerializeField] private int maxJumps = 2;
   // [SerializeField] private AudioSource walkAudio;
    [SerializeField] private AudioSource Jump;

    [SerializeField] private GameObject backgroundScroller;

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriterenderer;
   // public Transform attackpoint;

    Vector2 previousPosition;
    int JumpsRemaning = 2;
    float moving;
    bool jumping;
    bool isGrounded;
   
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Inputs();
        isJumping();
    }
    private void FixedUpdate()
    {
        isMoving();
    }
    void Inputs()
    {
        moving = Input.GetAxis("Horizontal");
        jumping = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || SimpleInput.GetButtonDown("JumpUp");
    }
    void isMoving()
    {
        checkplayerpositionforbackgroundscroll();

        if (moving > 0.01 || SimpleInput.GetButton("RightTurn"))
        {
            animator.SetFloat("IsWalking", 1f);
            transform.Translate(movementspeed * Time.deltaTime * Vector2.right);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(moving < 0 || SimpleInput.GetButton("LeftTurn"))
        {
            transform.localRotation = Quaternion.Euler(0, -180, 0);
            animator.SetFloat("IsWalking", 1f);
            transform.Translate(movementspeed * Time.deltaTime * Vector2.right);
        }
        else
        {
            animator.SetFloat("IsWalking", 0f);
        }
    }
    void isJumping()
    {
        if (jumping && JumpsRemaning >= 1)
        {
            animator.SetTrigger("takeoff");
            Jump.Play();
            rb2d.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            JumpsRemaning--;
        }
        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Paddle"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            // Check if the player is actually grounded by verifying if the collision is happening at the bottom of the player
            ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
            collision.GetContacts(contacts);
            foreach (ContactPoint2D contact in contacts)
            {
                if (contact.normal.y > 0.4f) // Adjust the threshold value if needed
                {
                    
                    JumpsRemaning = maxJumps;
                    break;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    private void checkplayerpositionforbackgroundscroll()
    {
        
        Vector2 currentPosition = transform.position;

        if (currentPosition.x > previousPosition.x)
        {
            backgroundScroller.GetComponent<BackgroundScroller>().scrollspeed = 0.5f;
        }
        else if (currentPosition.x < previousPosition.x)
        {
            backgroundScroller.GetComponent<BackgroundScroller>().scrollspeed = -0.5f;
        }
        else
        {
            backgroundScroller.GetComponent<BackgroundScroller>().scrollspeed = 0.0f;
        }
        previousPosition = currentPosition;
    }
}
