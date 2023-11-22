using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float maxSpeed;
    public float jumpSpeed;
    public float moveForce;
    Rigidbody2D rb;
    public bool isGrounded;
    public AudioClip jumpingSound;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(hor, 0) * moveForce) ;//judina kilograma

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            var source = GetComponent<AudioSource>();
            source.clip = jumpingSound;
            source.Play();
            Jump();
            
        }

        LimitSpeed();
        
    }

    void LimitSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed) //magnitude gauna speed
        {
            rb.drag = 1; //drag'as yra pasipriesinimas
        }
        else
        {
            rb.drag = 0;
        }
    }

    void Jump()
    {
        rb.velocity += Vector2.up * jumpSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.Lose();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

   void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.Win();
    }
}
