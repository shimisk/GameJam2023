using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CandyBullet : MonoBehaviour
{
    
    [SerializeField] float speed = 8f;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg  - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // Update is called once per frame
    void Update()
    {   
       transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
   
}
