﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{    

    // Start is called before the first frame update
    void Start()
    {
        // Set the vertical speed to make the bullet move upward
       /* Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 v = rb.velocity;
        v.y = 6;
        rb.velocity = v;*/
    }

    // Gets called when the object goes out of the screen
    void OnBecameInvisible()
    {
        // Destroy the bullet
        Destroy(gameObject);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Minotaur") || collision.CompareTag("Brainmole") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
