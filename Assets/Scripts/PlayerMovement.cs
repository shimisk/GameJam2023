using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public bool IsGrounded { get; set;}
    Vector3 dir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Rigidboy2D not found on " + gameObject.name);
        }
    
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRender component not found on " + gameObject.name);
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Aniamtor component not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CalculateMovement()
    {
        float hInput = Input.GetAxis("Horizontal");
        dir = new Vector3(hInput, 0, 0);
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (dir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Move()
    {   if (rb != null)
        {
            if (dir != Vector3.zero)
            {
                animator.SetBool("isWalking", true);
                rb.AddForce(dir * speed);
                if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                {
                    rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
                }
            }
            else if (IsGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetBool("isWalking", false);
            }
        }    
    }

    private void Jump()
    {
        IsGrounded = false;
        if (rb != null)
        {
            animator.SetTrigger("jumping");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    
}
