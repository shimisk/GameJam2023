using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] Transform rotationCenter;
    [SerializeField] float rotationRadius = 2f;
    [SerializeField] float angularSpeed = 2f;
    [SerializeField] float elipsemodifier = 2f;
    [SerializeField] float speedModifier;

    float posX, posY, angle = 0f;

    // Update is called once per frame
    void Update()
    {
        if (rotationCenter == null) { return; }
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius / elipsemodifier;
        transform.position = new Vector2(posX, posY);
        angle = angle + angularSpeed * Time.deltaTime;

        if(angle>= 360f)
        {
            angle = 0f; ;
        }

    }

    public void ChangeSpeed()
    {
        angularSpeed += angularSpeed * speedModifier; 
    }
}
