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
           Shoot();
           yield return new WaitForSeconds(Random.Range(minWait,maxWait));
        }
        yield return new WaitForSeconds(2f);
        while (_inCombat)
        {
            int index = Random.Range(0,bulletsPrefab.Length);
            Shoot(index);
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }
    }

    void Shoot(int index = 0)
    {
        Vector3 posToSpawn = new Vector3(transform.position.x, transform.position.y - 1.1f);
        GameObject bullet = Instantiate(bulletsPrefab[index], posToSpawn, Quaternion.identity);
        bullet.transform.localScale = transform.localScale;
    }

    public void SetInCombat()
    {
        _inCombat = true;
    }
}
