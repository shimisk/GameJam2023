using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatShooting : MonoBehaviour
{

    [SerializeField] GameObject[] bulletsPrefab;
    [SerializeField] float maxWait = 4f;
    [SerializeField] float minWait = 2f;

   
    bool _inCombat = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(BatShootingRoutine());
    }

    IEnumerator BatShootingRoutine()
    {
        while (!_inCombat)
        {
           Vector3 posToSpawn = new Vector3(transform.position.x, transform.position.y - 1.1f);
           GameObject bullet = Instantiate(bulletsPrefab[0], posToSpawn, Quaternion.identity);
           bullet.transform.localScale = transform.localScale;
           yield return new WaitForSeconds(Random.Range(minWait,maxWait));
        }
    }
}
