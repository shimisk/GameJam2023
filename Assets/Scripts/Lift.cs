using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    

    Vector3 _startPos;
   
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        
    }
}
