using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] AudioClip jumpClip;

    

    public bool IsGrounded { get; set;}
    public bool IsBeenHit {get; set;}
   
    Vector3 _dir = Vector3.zero;
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

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
        if (Mathf.Abs(_rb.velocity.x) > maxSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * maxSpeed, _rb.velocity.y);
        }
       
    }

    private void FixedUpdate()
    {
        if (IsBeenHit) { return; }
        
        if (_rb != null)
        {
            if (_dir != Vector3.zero)
            {
                Move();
                _animator.SetBool("Walking", true);
            }
            else if (IsGrounded)
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
                _animator.SetBool("Walking", false);

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
            _spriteRenderer.flipX = true;
        }
        else if (_dir.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void Jump()
    {
        IsGrounded = false;
        if (_rb != null)
        {
            if (jumpClip != null)
            {
                _audioSource.PlayOneShot(jumpClip);
            }
            _animator.SetTrigger("Jumping");
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {   
        _rb.AddForce(_dir * speed);  
    }

   
}
