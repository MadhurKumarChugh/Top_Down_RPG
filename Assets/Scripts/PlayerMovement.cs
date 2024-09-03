using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const string PLAYER_DOWN_IDLE = "player_down_idle";
    const string PLAYER_WALK_DOWN = "player_walk_down";
    const string WALK_UP = "walk_up";

    [Header("PLAYER MOVEMENT")]
    // Movement Speed of Player
    [SerializeField] float moveSpeed;
    
    // A movement vector to store input from user
    Vector2 movedirection;
    // Rigidbody object component of player
    Rigidbody2D playerBody;
    // Animator to control player animation by code
    Animator playerAnimator;
    private String currentState = PLAYER_DOWN_IDLE;
    
    void Awake()
    {
        // Getting the Rigidbody2D component of the player
        playerBody = GetComponent<Rigidbody2D>();
        // Getting the Animator component of the player
        playerAnimator = GetComponent<Animator>();
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
        
        if(moveY.Equals(1)) {AnimatePlayer(WALK_UP);}
        else if(moveY.Equals(-1)) {AnimatePlayer(PLAYER_WALK_DOWN);}
        else AnimatePlayer(PLAYER_DOWN_IDLE);
        
        
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
    
    // Function to control all animations
    public void AnimatePlayer(string newState)
    {
        if (currentState == newState) return;
        
        playerAnimator.Play(newState);
        
        currentState = newState;
    }

    

    // void Interact(float movX = 0, float movY = 0)
    // {
    //     var facingDirection = new Vector3(movX,movY);
    //     var interactPos = transform.position + facingDirection;
    //     Debug.DrawLine(transform.position, interactPos, Color.red,0.5f);
    // }
}
