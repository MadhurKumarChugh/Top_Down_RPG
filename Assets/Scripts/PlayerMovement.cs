using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PLAYER MOVEMENT")]
    // Movement Speed of Player
    [SerializeField] float moveSpeed;
    
    // A movement vector to store input from user
    Vector2 movedirection;
    // Rigidbody object component of player
    Rigidbody2D playerBody;
    // Animation Controller Script component of player
    AnimationController animationController;
    
    void Awake()
    {
        // Getting the Rigidbody2D component of the player
        playerBody = GetComponent<Rigidbody2D>();
        // Getting the Animator Controller Script component of the player
        animationController = GetComponent<AnimationController>();
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
        if (moveX != 0)
        {
            moveY = 0;
        }
        
        // Calling AnimatePlayer Function of Animation Controller Script
        animationController.AnimatePlayer(moveX, moveY);
        
        
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

    

    // void Interact(float movX = 0, float movY = 0)
    // {
    //     var facingDirection = new Vector3(movX,movY);
    //     var interactPos = transform.position + facingDirection;
    //     Debug.DrawLine(transform.position, interactPos, Color.red,0.5f);
    // }
}
