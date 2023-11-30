using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CandyBullet : MonoBehaviour
{
    
    [SerializeField] float speed = 8f;

    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {   
       transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
   
}
