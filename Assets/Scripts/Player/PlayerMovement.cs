using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : Player
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] AudioClip jumpClip;

    

    public bool IsGrounded { get; set;}
    public bool IsBeenHit {get; set;}
   
    Vector3 _dir = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        ClampVelocity();

        if (IsBeenHit) { return; }

        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }

    private void ClampVelocity()
    {
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
       
    }

    private void FixedUpdate()
    {
        if (IsBeenHit) { return; }
        
        if (rb != null)
        {
            if (_dir != Vector3.zero)
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
        _dir = new Vector3(hInput, 0, 0);
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (_dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (_dir.x > 0)
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
        rb.AddForce(_dir * speed);  
    }

   
}
