using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject lifes;
    public Sprite[] lifeSprites;

    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected AudioSource audioSource;
    protected Animator animator;

    public int Health { get; private set; }

    Image _currentLife;

    private void Awake()
    {
        Health = 3;
        GetAllComponents();
        gameOverMenu.SetActive(false);
        _currentLife = lifes.gameObject.GetComponent<Image>();
        _currentLife.sprite = lifeSprites[Health];
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
       
        _currentLife.sprite = lifeSprites[Health];
        if (Health < 1)
        {
            

            //pause game
            Time.timeScale = 0.0f;

            gameOverMenu.SetActive (true);
        }
    }

}
