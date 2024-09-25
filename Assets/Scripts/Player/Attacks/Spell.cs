using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    Rigidbody2D rb;
    Animator spellAnim;
    [SerializeField] float fireForce = 20f;
    [SerializeField] float lifeTime = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spellAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * (fireForce * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Spell"))
        {
            spellAnim.Play("Dark Hit");
            Destroy(gameObject, lifeTime);
        }
    }
}
