using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float period = 2f;

    float _speedModifier = 0.1f;
    Vector3 _startPos;


    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = UtilityHelper.SinWaveMovement(period, _startPos, direction,true);     
    }

    public void ChangeSpeed()
    {
        period -= period * (_speedModifier);
    }
}
