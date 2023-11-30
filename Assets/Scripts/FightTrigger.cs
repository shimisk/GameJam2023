using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    public GameObject bat;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
        
            bat.GetComponentInChildren<BatShooting>().SetInCombat();
            Destroy(gameObject, 2f);
        }
    }
}
