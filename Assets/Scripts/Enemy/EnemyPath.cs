using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [Header("ENEMY MOVEMENT")]
    [SerializeField] float movementSpeed = 2f;

    Rigidbody2D _enemybody;
    Vector2 _moveDir;

    void Awake()
    {
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
        _enemybody.MovePosition(_enemybody.position + _moveDir * (movementSpeed * Time.fixedDeltaTime));
    }

    // Target Position
    public void MoveTo(Vector2 targetPos)
    {
        _moveDir = targetPos;
    }
}
