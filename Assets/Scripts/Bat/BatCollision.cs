using UnityEngine;
using System.Collections;

public class BatCollision : MonoBehaviour
{
    [SerializeField] float timerToVulnerable = 0.5f;

    BatMovement bat;

    private bool _beenHit;


    private void Start()
    {
        bat = GetComponent<BatMovement>();
        if (bat == null)
        {
            Debug.LogError("BatMovement not found on " + gameObject.name);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (_beenHit) { return; }
            
            ChangeBatScale();
            if (transform.localScale.x < 0.1f)
            {
                Debug.Log("You win");
            }
            _beenHit = true;
            StartCoroutine(ResetHit());
            Destroy(collision.gameObject);
        }
    }

    private void ChangeBatScale()
    {
        Vector3 newScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f);
        transform.localScale = newScale;
        if (bat != null)
        {
            bat.ChangeSpeed();
        }
    }

    IEnumerator ResetHit()
    {  
        yield return new WaitForSeconds(timerToVulnerable);      
        _beenHit = false;
    }
}

