using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
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
    EAnimationController _animController;
    [SerializeField] float waitTime = 5f;
    int _rnd = 3;

    void Awake()
    {
        // Getting EnemyPath component of enemy
        _enemyPath = GetComponent<EnemyPath>();
        _animController = GetComponent<EAnimationController>();
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
            _rnd = Random.Range(1, 3);
            // Getting the roaming position of th enemy
            // and if enemy path is not null moving the
            // enemy to the new position 
            Vector2 roamPosition = GetRoamingPosition();
            if(_enemyPath) _enemyPath.Move(roamPosition);
            // Waiting for 2 seconds to repeat moving player
            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector2 GetRoamingPosition()
    {
        // Getting random directions for enemy to move to
        float moveX = Random.Range(-1.0f, 1.0f);
        float moveY = Random.Range(-1.0f, 1.0f);
        // Switch statement to restrict diagonal movement
        switch (_rnd)
        {
            case 1:
                moveX = 0;
                break;
            case 2:
                moveY = 0;
                break;
        }
        _animController.AnimateEnemy(moveX, moveY,_rnd);
        // TODO: Animate enemy based on moveX and moveY
        return new Vector2(moveX, moveY).normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Checking if Enemy has contacted player or not
        if (other.gameObject.CompareTag("Player"))
        {
            // Calling Interact to show messages
        }
    }
}
