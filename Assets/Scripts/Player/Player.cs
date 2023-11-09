using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected AudioSource audioSource;
    protected Animator animator;
    public float Health { get; private set; }

    private void Awake()
    {
        Health = 3;
        GetAllComponents();
    }
    

    private void GetAllComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Rigidboy2D not found on " + gameObject.name);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRender component not found on " + gameObject.name);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator controller not found on " + gameObject.name);
        }
    }

     public void GetDamaged()
    {
        Health--;
        Debug.Log("health" + Health);
        if (Health < 1)
        {
            Debug.Log("gameOver");
        }
    }

}
