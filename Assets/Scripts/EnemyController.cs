using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Checking if Enemy has contacted player or not
        if (other.gameObject.CompareTag("Player"))
        {
            // Calling Interact to show messages
            Interact();
        }
    }

    public void Interact()
    {
        // Showing Message
        Debug.Log("Enemy Interact");
    }
}
