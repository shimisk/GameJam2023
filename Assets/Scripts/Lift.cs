using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float period = 2f;

    Vector3 _startPos;
   
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cicle = Time.time / period;
        const float tau = Mathf.PI * 2;

        float rawSinWave = Mathf.Sin(cicle * tau);

        float sinWave = (rawSinWave + 1) / 2;
        transform.position = _startPos + direction * sinWave;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       Vector3 contactPoint = collision.GetContact(0).point;
       Vector3 vectorDir = collision.transform.position - contactPoint;

       //if above,set parent
       if(vectorDir.y > 0)
       {
            collision.transform.SetParent(gameObject.transform);
       }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
            collision.transform.parent = null;    
    }
}
