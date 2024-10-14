using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public bool isJumping;
    public bool isGrounded;
    public DeadScreen deadScreen;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private bool isDead = false;
    private int jumpCount;
    private const int maxJumpCount = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Die() {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        if (isGrounded)
        {
            jumpCount = 0;
        }
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.RightShift))
        {
            horizontalMovement *= 2;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < maxJumpCount))
        {
            isJumping = true;
            jumpCount++;
            animator.SetTrigger("isJump");
        }
        //on ajoute la touche pour attaquer
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("isAttack");
        }
        //on ajouter une touche pour tester la mort
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetBool("isDead", true);
            isDead = true;
            //wait for 2 seconds & call Die()
            Invoke("Die", 2f);
        }

        // Check if the player is out of camera bounds
        CheckOutOfStage();
        
    }

     void CheckOutOfStage()
    {
        
        Vector3 screenPosition = transform.position;
        // on log la position de l'objet 
        Debug.Log(screenPosition.y);
        if (screenPosition.y < -15)
        {
            Die();
        }

        // if (screenPosition.x > maxX) {
        //     Die();
        // }
    }

    void FixedUpdate()
    {
        MovePlayer(horizontalMovement);
        Flip(rb.velocity.x);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    void MovePlayer(float _horizontalMovement)
    {
        //on check si il est pas mort
        if (!isDead) {
        Vector3 targetVelocity = new Vector3(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = false;
            
            // //detect if the player is running
            // if (Input.GetKey(KeyCode.RightShift))
            // {
            //     rb.AddForce(new Vector2(0f, jumpForce * 1.5f));
            // } else {
            //     rb.AddForce(new Vector2(0f, jumpForce));
            // }
            // isJumping = false;
            // animator.SetTrigger("isJump");
        }
        }
    }

    void Flip(float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}