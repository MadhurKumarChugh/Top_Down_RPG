using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePointUp;
    public Transform firePointDown;
    public Transform firePointRight;
    public Transform firePointLeft;
    public GameObject spell;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) Fire();
    }

    void FixedUpdate()
    {
        
    }

    void Fire()
    {
        
            Quaternion rotation = Quaternion.Euler(0, 0, -90);
            Instantiate(spell, firePointUp.position, rotation);
        
    }
}
