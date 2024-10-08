using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PanimationController : MonoBehaviour
{
    [Header("ANIMATION CROSSFADE SETTINGS")] 
    [SerializeField] float normalCrossFadeTime = 0.2f;
    [SerializeField] float swordCrossFadeTime = 0.7f;

    // Sword variable to get & change the sword collider
    Sword _swordColl;
    // Variable to check sword attacking is playing or not
    bool _swordAtk;

    // Getter to get back the value of private var _swordAtk
    public bool SwordAtk => _swordAtk;

    // Animation State Names In The Animator Window of Unity
/************************************************PLAYER ANIMATIONS*****************************************************/
    private enum State
    {
        down_idle,
        up_idle,
        left_idle,
        right_idle,
        walk_down,
        walk_up,
        walk_left,
        walk_right,
        attack_down,
        attack_up,
        attack_left,
        attack_right,
        sword_attack_down,
        sword_attack_up,
        sword_attack_left,
        sword_attack_right,
    }
    
    const string DOWN_IDLE = "down_idle";

    // Animator to control player animation by code
    Animator _playerAnimator;

    // String to store current animation that is playing
    String _currentState = DOWN_IDLE;

    // Boolean to check Player is attacking or not
    private bool _isAttacking;

    // PlayerMovement Script Reference to constraint movement
    PlayerMovement _playerMovement;

    void Awake()
    {
        // Getting the Animator component of player
        _playerAnimator = GetComponent<Animator>();
        // Getting the PlayerMovement Script Component of player
        _playerMovement = GetComponent<PlayerMovement>();
        _swordColl = GetComponent<Sword>();
    }

    // void MouseTracker()
    // {
    //     Vector2 mousePos = Input.mousePosition;
    //     Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
    //     
    //     float diffX = mousePos.x - playerScreenPoint.x;
    //     float diffY = mousePos.y - playerScreenPoint.y;
    //     if(diffX > 0.0f && diffY > 0.0f) Debug.Log("QUAD 1");
    //     else if(diffX > 0.0f && diffY < 0.0f) Debug.Log("QUAD 4");
    //     else if(diffX < 0.0f && diffY < 0.0f) Debug.Log("QUAD 3");
    //     else if(diffX < 0.0f && diffY > 0.0f) Debug.Log("QUAD 2");
    //     
    //     // if(mousePos.x < playerScreenPoint.x) Debug.Log("LEFT");
    //     // else Debug.Log("RIGHT");
    //     
    //     // if(mousePosition.y < playerScreenPoint.y) Debug.Log("DOWN");
    //     // else Debug.Log("UP");
    // }

    // A Function to check if player is attacking 
    // and play attack animations
    public void AttackChecker(float xVal, float yVal)
    {
        SpellAttack(xVal, yVal);
        SwordAttack(xVal, yVal);
    }

    // Function to store the logic of which animation to play
    // and at what time/Input
    void AnimateStateNormal(string newState)
    {
        // This statement makes sure the same animation is not
        // played twice at the same time
        if (_currentState == newState) return;

        // Statement to change the current animation to the new
        // animation based on user input
        _currentState = newState;

        // Statement to play the animations
        _playerAnimator.CrossFade(newState, normalCrossFadeTime);
    }

    // Function to store the logic of which "SWORD" animation to play
    // and at what time/Input
    void AnimateStateSword(string newState)
    {
        // This statement makes sure the same animation is not
        // played twice at the same time
        if (_currentState == newState) return;

        // Statement to change the current animation to the new
        // animation based on user input
        _currentState = newState;

        // Statement to play the animations
        _playerAnimator.CrossFade(newState, swordCrossFadeTime);
    }

    // Function to animate player based on user input
    public void AnimatePlayer(float xDir, float yDir)
    {
        // if attack animation is playing then "return" will
        // not let the switch statement run thus locking the
        // animation at "attacking" and letting it complete before
        // "walk" animation can be played again
        if (_isAttacking) return;
        switch (xDir)
        {
            // switch statements checks to play walk animations
            // First for the LEFT and RIGHT WALK animations
            case 1:
                AnimateStateNormal(State.walk_right.ToString());
                break;
            case -1:
                AnimateStateNormal(State.walk_left.ToString());
                break;
            default:
            {
                if (_currentState == State.walk_right.ToString())
                {
                    AnimateStateNormal(State.right_idle.ToString());
                }
                else if (_currentState == State.walk_left.ToString())
                {
                    AnimateStateNormal(State.left_idle.ToString());
                }

                break;
            }
        }

        switch (yDir)
        {
            // Now switch checking for UP and DOWN WALK animations
            case 1:
                AnimateStateNormal(State.walk_up.ToString());
                break;
            case -1:
                AnimateStateNormal(State.walk_down.ToString());
                break;
            default:
            {
                if (_currentState == State.walk_up.ToString())
                {
                    AnimateStateNormal(State.up_idle.ToString());
                }
                else if (_currentState == State.walk_down.ToString())
                {
                    AnimateStateNormal(State.down_idle.ToString());
                }

                break;
            }
        }
    }

    void SpellAttack(float x, float y)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Player movement is stopped while attacking
            _playerMovement.Freeze();
            _isAttacking = true;
            // if-else checks for UP and DOWN ATTACK animations
            if (y.Equals(-1))
            {
                AnimateStateNormal(State.attack_down.ToString());
            }
            else if (y.Equals(1))
            {
                AnimateStateNormal(State.attack_up.ToString());
            }
            else if (y.Equals(0))
            {
                if (_currentState == State.down_idle.ToString())
                    AnimateStateNormal(State.attack_down.ToString());
                else if (_currentState == State.up_idle.ToString())
                    AnimateStateNormal(State.attack_up.ToString());
            }

            // if-else checks for LEFT and RIGHT ATTACK animations
            if (x.Equals(-1)) AnimateStateNormal(State.attack_left.ToString());
            else if (x.Equals(1)) AnimateStateNormal(State.attack_right.ToString());
            else if (x.Equals(0))
            {
                if (_currentState == State.left_idle.ToString())
                    AnimateStateNormal(State.attack_left.ToString());
                else if (_currentState == State.right_idle.ToString())
                    AnimateStateNormal(State.attack_right.ToString());
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // Player movement resumes while not attacking
            _playerMovement.UnFreeze();
            _isAttacking = false;
            // switch statement checks to play their idle animations respectively
            if (_currentState == State.attack_down.ToString())
            {
                if (x.Equals(0))
                {
                    AnimateStateNormal(State.down_idle.ToString());
                }
            }
            else if (_currentState == State.attack_up.ToString())
            {
                if (x.Equals(0))
                {
                    AnimateStateNormal(State.up_idle.ToString());
                }
            }
            else if (_currentState == State.attack_left.ToString())
            {
                if (y.Equals(0))
                {
                    AnimateStateNormal(State.left_idle.ToString());
                }
            }
            else if (_currentState == State.attack_right.ToString())
            {
                if (y.Equals(0))
                {
                    AnimateStateNormal(State.right_idle.ToString());
                }
            }
        }
    }

    void SwordAttack(float xd, float yd)
    {
        // Play animation at right click
        if (Input.GetMouseButtonDown(0) && !_swordColl.swordCollider.activeInHierarchy)
        {
            // Player movement is stopped while attacking
            _playerMovement.Freeze();
            _isAttacking = true;
            _swordAtk = true;
            // Activating Sword Collider
            _swordColl.swordCollider.SetActive(true);
            // if-else checks for UP and DOWN ATTACK animations
            if (yd.Equals(-1))
            {
                AnimateStateSword(State.sword_attack_down.ToString());
                _swordColl.RotateDown();
            }
            else if (yd.Equals(1))
            {
                AnimateStateSword(State.sword_attack_up.ToString());
                _swordColl.RotateUp();
            }
            else if (yd.Equals(0))
            {
                if (_currentState == State.down_idle.ToString())
                {
                    AnimateStateSword(State.sword_attack_down.ToString());
                    _swordColl.RotateDown();
                }
                else if (_currentState == State.up_idle.ToString())
                {
                    AnimateStateSword(State.sword_attack_up.ToString());
                    _swordColl.RotateUp();
                }
            }

            // if-else checks for LEFT and RIGHT ATTACK animations
            if (xd.Equals(-1)) {AnimateStateSword(State.sword_attack_left.ToString()); _swordColl.RotateLeft();}
            else if (xd.Equals(1)) {AnimateStateSword(State.sword_attack_right.ToString()); _swordColl.RotateRight();}
            else if (xd.Equals(0))
            {
                if (_currentState == State.left_idle.ToString())
                {AnimateStateSword(State.sword_attack_left.ToString()); _swordColl.RotateLeft();}
                else if (_currentState == State.right_idle.ToString())
                {AnimateStateSword(State.sword_attack_right.ToString()); _swordColl.RotateRight();}
            }
        }
        else if (Input.GetMouseButtonUp(0) && _swordColl.swordCollider.activeInHierarchy)
        {
            // Player movement resumes while not attacking
            _playerMovement.UnFreeze();
            _isAttacking = false;
            _swordAtk = false;
            // Deactivating Sword Collider and rotating it
            // its original down position
            _swordColl.swordCollider.SetActive(false);
            _swordColl.RotateDown();
            // switch statement checks to play their idle animations respectively
            if (_currentState == State.sword_attack_down.ToString())
            {
                if (xd.Equals(0))
                {
                    AnimateStateSword(State.down_idle.ToString());
                }
            }
            else if (_currentState == State.sword_attack_up.ToString())
            {
                if (xd.Equals(0))
                {
                    AnimateStateSword(State.up_idle.ToString());
                }
            }
            else if (_currentState == State.sword_attack_left.ToString())
            {
                if (yd.Equals(0))
                {
                    AnimateStateSword(State.left_idle.ToString());
                }
            }
            else if (_currentState == State.sword_attack_right.ToString())
            {
                if (yd.Equals(0))
                {
                    AnimateStateSword(State.right_idle.ToString());
                }
            }
        }
    }
}