using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyPath : MonoBehaviour
{
    [Header("ENEMY MOVEMENT")]
    [SerializeField] float movementSpeed = 2f;

    Rigidbody2D _enemybody;
    // Vector to hold movement direction of enemy
    Vector2 _moveDir;

    void Awake()
    {
        // Getting Rigidbody component of enemy
        _enemybody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Moving enemy using velocity
        _enemybody.MovePosition(_enemybody.position + _moveDir * (movementSpeed * Time.fixedDeltaTime));
    }

    // Target Position
    public void Move(Vector2 targetDir)
    {
        // Setting the new movement direction to target position
        _moveDir = targetDir;
    }

    public void Stop()
    {
        _enemybody.MovePosition(Vector2.zero);
    }
    
}
