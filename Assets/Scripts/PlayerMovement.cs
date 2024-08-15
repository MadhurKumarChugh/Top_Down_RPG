using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 movedirection;

    Rigidbody2D playerBody;
    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Processing Input
        ProcessInput();
    }

    void FixedUpdate()
    {
        //Physics Calculations
        Move();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movedirection = new Vector2(moveX, moveY);
        movedirection.Normalize();
    }

    void Move()
    {
        playerBody.velocity = new Vector2(movedirection.x * moveSpeed, movedirection.y * moveSpeed);
    }
}
