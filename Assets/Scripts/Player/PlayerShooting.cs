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
    public GameObject spellDown;
    public GameObject spellLeft;
    [SerializeField] float cooldown = 2f;
    
    float cooldownTimer;
    Transform firePoint;

    void Awake()
    {
        firePoint = firePointDown;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) FireChecker();
    }

    void FixedUpdate()
    {
        
    }

    public void FirePointer(float xPnt, float yPnt)
    {
        if (xPnt.Equals(1)) firePoint = firePointRight;
        else if (xPnt.Equals(-1)) firePoint = firePointLeft;
        else if (yPnt.Equals(1)) firePoint = firePointUp;
        else if (yPnt.Equals(-1)) firePoint = firePointDown;
    }

    void FireChecker()
    {
        if(firePoint == firePointDown) FireDown();
        else if(firePoint == firePointLeft) FireLeft();
        else Fire();
    }

    void Fire()
    {
        cooldownTimer -= Time.deltaTime;
        
        if (cooldownTimer > 0) return;
        
        cooldownTimer = cooldown;
        Instantiate(spell, firePoint.position, firePoint.rotation);
    }

    void FireDown()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, -90);
        cooldownTimer -= Time.deltaTime;
        
        if (cooldownTimer > 0) return;
        
        cooldownTimer = cooldown;
        Instantiate(spellDown, firePoint.position, rotation);
    }

    void FireLeft()
    {
        Quaternion rotation = Quaternion.Euler(0, 180, 0);
        cooldownTimer -= Time.deltaTime;
        
        if (cooldownTimer > 0) return;
        
        cooldownTimer = cooldown;
        Instantiate(spellLeft, firePoint.position, rotation);
    }
}
