using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PLAYER MOVEMENT")]
    // Movement Speed of Player
    [SerializeField] float moveSpeed;
    
    // A movement vector to store input from user
    Vector2 _movedirection;
    // Rigidbody object component of player
    Rigidbody2D _playerBody;
    // Animation Controller Script component of player
    PanimationController _animationController;
    // Player Shooting Script component of player 
    PlayerShooting _shooting;
    
    void Awake()
    {
        // Getting the Rigidbody2D component of the player
        _playerBody = GetComponent<Rigidbody2D>();
        // Getting the Animator Controller Script component of the player
        _animationController = GetComponent<PanimationController>();
        // Getting the Player Shooting Script component
        _shooting = GetComponent<PlayerShooting>();
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
        _animationController.AnimatePlayer(moveX, moveY);
        _animationController.AttackChecker(moveX, moveY);
        // Calling FirePointer Function of Player Shooting Script
        _shooting.FirePointer(moveX, moveY);
        
        
        // Storing the input in movedirection vector
        _movedirection = new Vector2(moveX, moveY);
        
        // To normalize diagonal movement
        // If not implemented, diagonal movement will be higher than left-right,up-down movement
        _movedirection.Normalize();
    }

    void Move()
    {
        // Using velocity to move player
        _playerBody.velocity = new Vector2(_movedirection.x * (moveSpeed * Time.fixedDeltaTime), 
            _movedirection.y * (moveSpeed * Time.fixedDeltaTime));
    }

    // Function to restrict player movement
    public void Freeze()
    {
        _playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Function to resume player movement
    public void UnFreeze()
    {
        _playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    

    // void Interact(float movX = 0, float movY = 0)
    // {
    //     var facingDirection = new Vector3(movX,movY);
    //     var interactPos = transform.position + facingDirection;
    //     Debug.DrawLine(transform.position, interactPos, Color.red,0.5f);
    // }
}
