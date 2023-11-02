using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;

    Rigidbody2D rb;

    public bool IsGrounded { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        if (rb == null)
        {
            Debug.Log("Rigidboy2D not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        IsGrounded = false;
        if (rb != null)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CalculateMovement()
    {
        float hInput = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(hInput, 0);
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
