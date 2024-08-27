using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // walkDown Parameter of Animator
    private static readonly int WalkDown = Animator.StringToHash("walkDown");
    // isAttacking Parameter of Animator
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    [Header("PLAYER MOVEMENT")]
    // Movement Speed of Player
    [SerializeField] float moveSpeed;
    
    // A movement vector to store input from user
    Vector2 movedirection;
    // Rigidbody object component of player
    Rigidbody2D playerBody;
    // Animator to control player animation by code
    Animator playerAnimator;
    
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
        
        AnimatePlayer(moveX, moveY);
        
        
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
    void AnimatePlayer(float x, float y)
    {
        DownAnim(y);
        AttackChecker();
    }

    // Function to check if player is attacking or not
    void AttackChecker()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetBool(IsAttacking,true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimator.SetBool(IsAttacking,false);
        }
    }

    // Function for WalkDown animation
    void DownAnim(float y)
    {
        if (y.Equals(-1))
        {
            playerAnimator.SetBool(WalkDown, true);
        }
        else if (y.Equals(0))
        {
            playerAnimator.SetBool(WalkDown, false);
        }
    }

    // void Interact(float movX = 0, float movY = 0)
    // {
    //     var facingDirection = new Vector3(movX,movY);
    //     var interactPos = transform.position + facingDirection;
    //     Debug.DrawLine(transform.position, interactPos, Color.red,0.5f);
    // }
}
