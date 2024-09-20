using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    // Waiting time before ShowMessage coroutine starts
    [SerializeField] float waitTime = 2f;
    // A boolean to check we have contacted the Player or not
    bool isTouched;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if key is pressed after contacting Player
        if (Input.GetKey(KeyCode.Space) && isTouched)
        {
            // Calling Interact to start showing messages
            Interact();
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        // Checking if NPC has contacted player or not
        if (other.gameObject.CompareTag("Player"))
        {
            // If NPC has contacted player,
            // isTouched is set to true
            isTouched = true;
        }
    }
    
    IEnumerator Interacting()
    {
        int count = 0;
        // Debug.Log("Startrd");
        // Started loop of coroutine if contacted NPC
        // Can be changed to a for loop later on
        while(isTouched)
        {
            count++;
            Debug.Log(count);
            // Waiting before next message is shown
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void Interact()
    {
        // Started Show Message coroutine
        StartCoroutine(Interacting());
    }

    public void Move()
    {
        
    }

    public void TakeDamage()
    {
        
    }
}
