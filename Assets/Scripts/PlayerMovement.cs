using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement Speed of Player
    [SerializeField] float moveSpeed;
    // A boolean to check we have contacted the NPC or not
    bool isTouched;
    // Waiting time before ShowMessage coroutine starts
    [SerializeField] float waitTime = 2f;
    
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
        
        // Checking if key is pressed after contacting NPC
        if (Input.GetKey(KeyCode.Space) && isTouched)
        {
            // Started Show Message coroutine
            StartCoroutine(Interact());
        }
    }

    void FixedUpdate()
    {
        //Physics Calculations
        Move();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Checking if player has contacted NPC or not
        if (other.gameObject.CompareTag("NPC"))
        {
            // If player has contacted NPC, isTouched is set to true
            isTouched = true;
        }
    }
    
    IEnumerator Interact()
    {
        int count = 0;
        // Debug.Log("Startrd");
        // Started loop of coroutine if contacted NPC
        // Can be changed to a for loop later on
        while(isTouched)
        {
            // Waiting before next message is shown
            yield return new WaitForSeconds(waitTime);
            count++;
            Debug.Log(count);
        }
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
