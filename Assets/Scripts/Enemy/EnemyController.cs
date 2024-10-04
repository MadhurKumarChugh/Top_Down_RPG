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
        Roaming,
        Chasing
    }

    // Variable of type State enum
    State _state;

    // Variable of type EnemyPath
    EnemyPath _enemyPath;
    EAnimationController _animController;
    [SerializeField] float waitTime = 5f;
    int _rnd = 3;
    float _moveX;
    float _moveY;

    [Header("ENEMY Health")] 
    [SerializeField] float startingHealth = 3f;

    float _currentHealth;


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
        _currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StopCoroutine(Roaming());
        if(_enemyPath) _enemyPath.IsChasing = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(Roaming());
        if(_enemyPath) _enemyPath.IsChasing = false;
    }

    // Coroutine to handle enemy movement/roaming state
    IEnumerator Roaming()
    {
        // while the player is roaming do the following
        while (_state == State.Roaming)
        {
            _rnd = Random.Range(1, 4);
            // Getting the roaming position of th enemy
            // and if enemy path is not null moving the
            // enemy to the new position 
            Vector2 roamPosition = GetRoamingPosition();
            if (_enemyPath) _enemyPath.Move(roamPosition);
            // Waiting for 2 seconds to repeat moving player
            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector2 GetRoamingPosition()
    {
        // Getting random directions for enemy to move to
        _moveX = Random.Range(-1.0f, 1.0f);
        _moveY = Random.Range(-1.0f, 1.0f);
        // Switch statement to restrict diagonal movement
        if (_rnd == 1) _moveX = 0;
        else if (_rnd == 2) _moveY = 0;
        else if (_rnd == 3)
        {
            _moveX = 0;
            _moveY = 0;
            if (_enemyPath) _enemyPath.Stop();
        }

        if (_animController)
        {
            _animController.GetVectors(_moveX, _moveY);
            _animController.AnimateEnemy(_rnd);
        }

        return new Vector2(_moveX, _moveY).normalized;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         _rnd = 3;
    //         _enemyPath.Stop();
    //         _animController.AnimateEnemy(_rnd);
    //     }
    // }
    //
    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     _rnd = 3;
    //     _animController.AnimateEnemy(_rnd);
    // }

    public void Interact()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Destroy(gameObject);
    }
}