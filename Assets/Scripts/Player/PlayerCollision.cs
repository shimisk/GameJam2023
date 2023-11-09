using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : Player
{
    [SerializeField] float repelForce = 2.5f;
    [SerializeField] float timerToEnableInput = 0.75f;
    [SerializeField] AudioClip impactClip;

    PlayerMovement player;

    private void Start()
    {
      player = GetComponent<PlayerMovement>();
        if (player == null)
        {
            Debug.LogError("PlayerMovement not found on " + gameObject.name);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
           
            if (player != null)
            {
                 player.IsGrounded = true;
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //if is hit return
            if (player.IsBeenHit) { return;}
            
            player.IsBeenHit = true;
            GetDamaged();
            CalculateRepelForce(collision);
            StartCoroutine(ResetHit());
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            //if is hit return
            if (player.IsBeenHit)
            {
                Destroy(collision.gameObject);
                return;
            }

            player.IsBeenHit = true;
            GetDamaged();
            CalculateRepelForce(collision);
            Destroy(collision.gameObject);
            StartCoroutine(ResetHit());
        }
    }

    private void CalculateRepelForce(Collision2D collision)
    {
        Vector3 contactPoint = collision.GetContact(0).point;
        Vector3 vectorDir = (transform.position - contactPoint);
        if (rb != null)
        {
            Vector2 forceDir = new Vector2(Mathf.Sign(vectorDir.x) * 1, 1);
            rb.AddForce(forceDir * repelForce, ForceMode2D.Impulse);
        }
        if (impactClip != null)
        {
            audioSource.PlayOneShot(impactClip);
        }
    }

    IEnumerator ResetHit()
    {
        animator.SetBool("IsHurt", true);
        yield return new WaitForSeconds(timerToEnableInput);
        animator.SetBool("IsHurt", false);
        player.IsBeenHit= false;
    }
}
