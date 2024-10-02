using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _spellAnim;
    [SerializeField] float fireForce = 20f;
    [SerializeField] float lifeTime = 1f;
    
    [SerializeField] float spellDamage = 1f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spellAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Moving the Spell object using Rigidbody.velocity
        _rb.velocity = transform.right * (fireForce * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // If not collided with player then
        if (!other.gameObject.CompareTag("Player"))
        {
            // Play dark spell hit animation and
            // destroy object after certain life-time
            _spellAnim.Play("Dark Hit");
            Destroy(gameObject, lifeTime);
            if (other.gameObject.CompareTag("Enemy"))
            {
                var path = other.gameObject.GetComponent<EnemyController>();
                path.TakeDamage(spellDamage);
            }
        }
        // else if collided with another spell object
        else if (other.gameObject.CompareTag("Spell"))
        {
            // Play and destroy immediately
            _spellAnim.Play("Dark Hit");
            Destroy(gameObject);
        }
    }
}
