using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] Vector3 scaleTarget;
    [SerializeField] float scaleSpeed;

    SpriteRenderer spriteRenderer;

    bool _isScaled = false;
    bool _isTransitioning = false;
    Vector3 _startingScale;


    // Start is called before the first frame update
    void Start()
    {
        _startingScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
           Debug.LogError("SpriteRender not found on " + gameObject.name);
        }
    }

    private void Update()
    {
        if (_isTransitioning && !_isScaled)
        {
            ScaleTransition(scaleTarget);

        }
        else if (_isTransitioning && _isScaled)
        {
            ScaleTransition(_startingScale);
        }

        if(transform.localScale == _startingScale)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }
            Destroy(collision.gameObject);
            _isTransitioning = true;
        }
    }

    void ScaleTransition(Vector3 scaleToApply)
    {
        transform.localScale = Vector3.Lerp(transform.localScale, scaleToApply, scaleSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.localScale, scaleToApply) < 0.05f)
        {
            _isScaled = !_isScaled;
            _isTransitioning = false;
            transform.localScale = scaleToApply;
        }
    }

}
