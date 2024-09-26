using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Currently moving player using MovePosition Function
        // TODO: Might change in the future to move using _enemybody.velocity 
        _enemybody.MovePosition(_enemybody.position + _moveDir * (movementSpeed * Time.fixedDeltaTime));
    }

    // Target Position
    public void MoveTo(Vector2 targetPos)
    {
        // Setting the new movement direction to target position
        _moveDir = targetPos;
    }
}
