using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMini : MonoBehaviour
{
    
    public bool HasTarget {  get; set; }
    
    Animator animator;
    Collider2D col;

    GameObject _target;
    float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindWithTag("Player");
        _speed = Random.Range(3f, 5f);
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(HasTarget)
        {
            col.enabled = true;
            animator.SetBool("HasTarget", true);
            Vector3 targetPos = new Vector3(_target.transform.position.x, _target.transform.position.y + 2, 0);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            ScoreManager.Instance.Addscore(1000);
          
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
