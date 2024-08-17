using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement Speed of Player
    [SerializeField] float moveSpeed;
    bool interacted = false;
    bool isTouched = false;
    
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
        ShowMessage();
    }

    void FixedUpdate()
    {
        //Physics Calculations
        Move();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            isTouched = true;
        }
    }
    
    // void OnCollisionStay2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("NPC") && interacted)
    //     {
    //         Debug.Log("NPC is saying something");
    //     }
    // }
    
    // void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("NPC") && interacted)
    //     {
    //         Debug.Log("NPC is saying something");
    //     }
    // }

    IEnumerator Interact()
    {
        int count = 0;
        // Debug.Log("Startrd");
        while(interacted && isTouched)
        {
            yield return new WaitForSeconds(0);
            count++;
            Debug.Log(count);
        }
    }

    void ShowMessage()
    {
        StartCoroutine(Interact());
    }
    

    

    void ProcessInput()
    {
        // Getting input from user
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            interacted = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            interacted = false;
        }
        
        // *****OPTIONAL*****
        // Cancel diagonal movement
        if (moveX != 0)
        {
            moveY = 0;
        }

        
        // Interact(moveX, moveY);
        
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
