using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, Interactable
{
    // States of enemy
    private enum State
    {
        Roaming
    }

    // Variable of type State enum
    State _state;
    // Variable of type EnemyPath
    EnemyPath _enemyPath;

    void Awake()
    {
        // Getting EnemyPath component of enemy
        _enemyPath = GetComponent<EnemyPath>();
        // Setting the current state to roaming
        _state = State.Roaming;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Starting the Roaming Coroutine
        StartCoroutine(Roaming());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Coroutine to handle enemy movement/roaming state
    IEnumerator Roaming()
    {
        // while the player is roaming do the following
        while (_state == State.Roaming)
        {
            // Getting the roaming position of th enemy
            // and if enemy path is not null moving the
            // enemy to the new position 
            Vector2 roamPosition = GetRoamingPosition();
            if(_enemyPath) _enemyPath.MoveTo(roamPosition);
            // Waiting for 2 seconds to repeat moving player
            yield return new WaitForSeconds(2f);
        }
    }

    Vector2 GetRoamingPosition()
    {
        // Getting random directions for enemy to move to
        float moveX = Random.Range(-1.0f, 1.0f);
        float moveY = Random.Range(-1.0f, 1.0f);
        // TODO: Animate enemy based on moveX and moveY
        return new Vector2(moveX, moveY).normalized;
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

    public void Move()
    {
        
    }

    public void TakeDamage()
    {
        
    }
}
