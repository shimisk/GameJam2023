using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] AudioClip jumpClip;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    AudioSource audioSource;

    public bool IsGrounded { get; set;}
    Vector3 dir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        GetAllComponents();
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
        if (rb != null)
        {
            if (dir != Vector3.zero)
            {
                Move();
                animator.SetBool("Walking", true);
            }
            else if (IsGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetBool("Walking", false);

            }
        }
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

    private void Jump()
    {
        IsGrounded = false;
        if (rb != null)
        {
            if (jumpClip != null)
            {
                audioSource.PlayOneShot(jumpClip);
            }
            animator.SetTrigger("Jumping");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        
        rb.AddForce(dir * speed);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    private void GetAllComponents()
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

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator controller not found on " + gameObject.name);
        }
    }
}
