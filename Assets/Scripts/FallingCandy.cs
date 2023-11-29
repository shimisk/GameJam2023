using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCandy : MonoBehaviour
{
    [SerializeField] float gravity = 1f;
    [SerializeField] float fallingRate = 5f;


    Rigidbody2D rb;
  
    Vector3 _startPos;
    public bool _canFall = false;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        }
        StartCoroutine(ResetCanFallRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(_canFall)
        {
            rb.gravityScale = gravity;
        }
        else 
        { 
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        transform.position = _startPos;
        _canFall = false;
        StartCoroutine(ResetCanFallRoutine());
    }

    IEnumerator ResetCanFallRoutine() { 
        yield return new WaitForSeconds(fallingRate);
        _canFall = true;
    }
}
