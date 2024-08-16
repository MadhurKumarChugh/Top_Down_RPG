using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement Speed of Player
    [SerializeField] float moveSpeed;
    
    // A movement vector to store input from user
    [SerializeField] Vector2 movedirection;
    
    // Rigidbody object component of player
    Rigidbody2D playerBody;
    
    void Awake()
    {
        // Getting the Rigidbody2D component of the player
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
        // Getting input from user
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        // *****OPTIONAL*****
        // Cancel diagonal movement
        // if (moveX != 0)
        // {
        //     moveY = 0;
        // }
        
        // Storing the input in movedirection vector
        movedirection = new Vector2(moveX, moveY);
        
        // To normalize diagonal movement
        // If not implemented, diagonal movement will be higher than left-right,up-down movement
        movedirection.Normalize();
    }

    void Move()
    {
        // Using velocity to move player
        playerBody.velocity = new Vector2(movedirection.x * moveSpeed, movedirection.y * moveSpeed);
    }
}
