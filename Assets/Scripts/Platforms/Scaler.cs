using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public enum ScaleStates
{
    normal,
    scaled
}
public class Scaler : MonoBehaviour
{
    [SerializeField] float scaleSize;
    [SerializeField] bool scaleOnX;
    [SerializeField] bool scaleOnY;
    [SerializeField] float scaleSpeed;
    [SerializeField] bool autoShrink;
    [SerializeField] float autoShrinkWait = 1;
    [SerializeField] bool isVisible;
    [SerializeField] bool autoScale;

    ScaleStates scaleState = ScaleStates.normal;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    
    bool _startTransition = false;
    bool _startShrinking = false;
    Vector2 _startingScale;
    Vector2 _scaleTarget;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BocCollider2D component not found on " + gameObject.name);
        }
       

            if (spriteRenderer != null)
            {
                if (!isVisible)
                {
                spriteRenderer.enabled = false;
                }
                _startingScale = spriteRenderer.size;
                CalculateScaleToApply();
                
            }
            else
            {
                Debug.LogError("SpriteRender not found on " + gameObject.name);
            }
        
       
    }

    
    private void Update()
    { 
        if (scaleState == ScaleStates.normal && _startTransition)
        {
            ScaleTransition(_scaleTarget);
        }

        if (!autoShrink && scaleState == ScaleStates.scaled && _startTransition)
        {
            ScaleTransition(_startingScale);
        }

        if (_startShrinking)
        {
            ScaleTransition(_startingScale);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            boxCollider.isTrigger = false;
            if(spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }
            if(boxCollider != null)
            {
                boxCollider.isTrigger = false;  
            }
            _startTransition = true;
            Destroy(collision.gameObject);    
        }
    }

    private void CalculateScaleToApply()
    {
        if (scaleOnX && scaleOnY)
        {
            _scaleTarget = new Vector2(scaleSize, scaleSize);
        }
        else if (scaleOnX)
        {
            _scaleTarget = new Vector2(scaleSize, _startingScale.y);
        }
        else if (scaleOnY)
        {
            _scaleTarget = new Vector2(_startingScale.x, scaleSize);
        }
    }


    void ScaleTransition(Vector3 scaleToApply)
    {
        spriteRenderer.size = Vector2.Lerp(spriteRenderer.size, scaleToApply, scaleSpeed * Time.deltaTime);
        if (Vector2.Distance(spriteRenderer.size, scaleToApply) < 0.05f)
        {
            
            if(scaleState == ScaleStates.normal)
            {
                scaleState = ScaleStates.scaled;
                if (autoShrink)
                {
                    StartCoroutine(StartShrinkingRoutine());
                }
            }
            else
            {
                scaleState = ScaleStates.normal;
                if (spriteRenderer != null && !isVisible)
                {
                    spriteRenderer.enabled = false;
                }
                if (boxCollider != null)
                {
                    boxCollider.isTrigger = true;
                }
                _startShrinking = false;
            }
            _startTransition = false;
            spriteRenderer.size = scaleToApply;
        }
    }

    IEnumerator StartShrinkingRoutine()
    {
        yield return new WaitForSeconds(autoShrinkWait);
        _startShrinking = true;
    }

}
