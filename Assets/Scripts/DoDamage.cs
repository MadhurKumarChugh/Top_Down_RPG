using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
   [SerializeField] float damage = 1f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var path = other.gameObject.GetComponent<EnemyController>();
            path.TakeDamage(damage);
        }
    }
}
