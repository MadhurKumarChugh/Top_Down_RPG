using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Object to set sword collider on or off
    // in Panimation Script 
    public GameObject swordCollider;

/**********************FUNCTIONS TO ROTATE SWORD COLLIDER BASED ON THE DIRECTION PLAYER IS FACING**********************/    
    public void RotateLeft()
    {
        swordCollider.transform.rotation = Quaternion.Euler(0, 0, -90);
    }
    
    public void RotateRight()
    {
        swordCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
    
    public void RotateUp()
    {
        swordCollider.transform.rotation = Quaternion.Euler(0, 0, 180);
    }
    
    public void RotateDown()
    {
        swordCollider.transform.rotation = Quaternion.identity;
    }
}
