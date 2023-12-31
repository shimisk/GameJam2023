using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _playerCollision : MonoBehaviour
{
    [SerializeField] float repelForce = 2.5f;
    [SerializeField] float timerToEnableInput = 0.75f;
    [SerializeField] AudioClip impactClip;
    [SerializeField] AudioClip collectClip;   

    private PlayerMovement _playerMovement;
    private Player _player;
    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private Animator _animator;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        if (_playerMovement == null)
        {
            Debug.LogError("_playerMovement not found on " + gameObject.name);
        }

        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
           
            if (_player != null)
            {
                 _playerMovement.IsGrounded = true;
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //if is hit return
            if (_playerMovement.IsBeenHit) { return;}
            
            GetHit(collision);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            //if is hit return
            if (_playerMovement.IsBeenHit)
            {
                Destroy(collision.gameObject);
                return;
            }
            
            GetHit(collision);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (_playerMovement.IsBeenHit) { return; }
           
            GetHit(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Friend"))
        {
            Destroy(collision.gameObject);
            _audioSource.PlayOneShot(collectClip);
            ScoreManager.Instance.Addscore(5000);
        }
    }

    private void GetHit(Collision2D collision)
    {
        _playerMovement.IsBeenHit = true;
        _player.GetDamaged();
        CalculateRepelForce(collision);
        StartCoroutine(ResetHit());
        ScoreManager.Instance.Addscore(-500);
    }

   


    private void CalculateRepelForce(Collision2D collision)
    {
        Vector3 contactPoint = collision.GetContact(0).point;
        Vector3 vectorDir = (transform.position - contactPoint);
        if (_rb != null)
        {
            Vector2 forceDir = new Vector2(Mathf.Sign(vectorDir.x) * 1, 1);
            _rb.AddForce(forceDir * repelForce, ForceMode2D.Impulse);
        }
        if (impactClip != null)
        {
            _audioSource.PlayOneShot(impactClip);
        }
    }

    IEnumerator ResetHit()
    {
        _animator.SetBool("IsHurt", true);
        yield return new WaitForSeconds(timerToEnableInput);
        _animator.SetBool("IsHurt", false);
        _playerMovement.IsBeenHit= false;
    }
}
