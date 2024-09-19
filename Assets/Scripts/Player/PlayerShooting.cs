using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    // These are points from where our player will fire
    // based on which direction player is facing
    [Header("PLAYER FIRING POINTS")]
    public Transform firePointUp;
    public Transform firePointDown;
    public Transform firePointRight;
    public Transform firePointLeft;
    
    // These are spells that will we fired (instantiated)
    [Header("SPELL PROJECTILES")]
    public GameObject spell;
    public GameObject spellDown;
    public GameObject spellLeft;
    
    // Cooldown time before next spelled is fired
    [SerializeField] float cooldown = 2f;
    // Point from which the spell will be fired 
    Transform _firePoint;
    // Variable to store which coroutine is running
    Coroutine fire;

    void Awake()
    {
        // Setting default firing point to down
        _firePoint = firePointDown;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        
    }

    // Function to process user input
    void ProcessInput()
    {
        // if space is pressed start firing
        if (Input.GetKeyDown(KeyCode.Space)) FireChecker();
        // otherwise stop firing
        else if (Input.GetKeyUp(KeyCode.Space)) StopCoroutine(fire);
    }

    // Function to set the firing point to whatever
    // direction the player is facing
    public void FirePointer(float xPnt, float yPnt)
    {
        if (xPnt.Equals(1)) _firePoint = firePointRight;
        else if (xPnt.Equals(-1)) _firePoint = firePointLeft;
        else if (yPnt.Equals(1)) _firePoint = firePointUp;
        else if (yPnt.Equals(-1)) _firePoint = firePointDown;
    }

    // Function to check which direction player is facing
    // and start firing coroutine accordingly 
    void FireChecker()
    {
        if (_firePoint == firePointDown) fire = StartCoroutine(FireDown());
        else if (_firePoint == firePointLeft) fire = StartCoroutine(FireLeft());
        else fire = StartCoroutine(FireUpAndRight());
    }

    // Fire Down Coroutine
    IEnumerator FireDown()
    {
        while (true)
        {
            // Rotate downwards
            Quaternion rotation = Quaternion.Euler(0, 0, -90);
            Instantiate(spellDown, _firePoint.position, rotation);
            yield return new WaitForSeconds(cooldown);
        }
    }
    
    // Fire Up and Right Coroutine
    IEnumerator FireUpAndRight()
    {
        while (true)
        {
            // No rotation
            Instantiate(spell, _firePoint.position, _firePoint.rotation);
            yield return new WaitForSeconds(cooldown);
        }
    }
    
    // Fire Left Coroutine
    IEnumerator FireLeft()
    {
        while (true)
        {
            // Rotate towards left
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            Instantiate(spellLeft, _firePoint.position, rotation);
            yield return new WaitForSeconds(cooldown);
        }
    }
}
