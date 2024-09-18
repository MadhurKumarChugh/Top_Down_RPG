using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    Rigidbody2D rb;
    public float fireForce = 20f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = -transform.right * fireForce;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject, 2f);
    }
}
