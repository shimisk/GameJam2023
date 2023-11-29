using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTrigger : MonoBehaviour
{
    public GameObject batsToTrig;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BatMini[] bats = batsToTrig.GetComponentsInChildren<BatMini>();
            if (bats == null) { return; }
           
            foreach (BatMini bat in batsToTrig.GetComponentsInChildren<BatMini>()){
                bat.HasTarget = true;
            }
        }
    }
}
