using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject aim;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate;

    float nextShot = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aiming();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            GameObject b = Instantiate(bullet, transform.position, aim.transform.rotation);
            //rotate to correct aim
            b.transform.Rotate(0, 0, -90);
        }
        
    }

    private void Aiming()
    {
        //get the rotating point in camra coordinates
        Vector2 pivot = Camera.main.WorldToScreenPoint(aim.transform.position);
        //find direction of the vector
        Vector2 dir = (Vector2)Input.mousePosition - pivot;
        //calcolate angle of the vector,change it to degree
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //set roatation
        aim.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
    }
}
