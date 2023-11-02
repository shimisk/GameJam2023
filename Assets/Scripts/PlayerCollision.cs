using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            PlayerMovement player = gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                 player.IsGrounded = true;
            }
        }
    }
}
