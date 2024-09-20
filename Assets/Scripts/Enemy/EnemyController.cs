using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, Interactable
{
    private enum State
    {
        Roaming
    }

    State _state;
    EnemyPath _enemyPath;

    void Awake()
    {
        _enemyPath = GetComponent<EnemyPath>();
        _state = State.Roaming;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Roaming());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Roaming()
    {
        while (_state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            if(_enemyPath) _enemyPath.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
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
