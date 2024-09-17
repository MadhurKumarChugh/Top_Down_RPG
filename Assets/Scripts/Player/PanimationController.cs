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
    
     // Animation State Names In The Animator Window of Unity
/************************************************PLAYER ANIMATIONS*****************************************************/
        /************IDLE************/
        const string DOWN_IDLE = "down_idle";
        const string UP_IDLE = "up_idle";
        const string LEFT_IDLE = "left_idle";
        const string RIGHT_IDLE = "right_idle";
        /************WALK************/
        const string WALK_DOWN = "walk_down";
        const string WALK_UP = "walk_up";
        const string WALK_LEFT = "walk_left";
        const string WALK_RIGHT = "walk_right";
        /************ATTACK************/
        const string ATTACK_DOWN = "attack_down";
        const string ATTACK_UP = "attack_up";
        const string ATTACK_LEFT = "attack_left";
        const string ATTACK_RIGHT = "attack_right";
        /************SWORD ATTACK************/
        const string SWORD_ATTACK_DOWN = "sword_attack_down";
        const string SWORD_ATTACK_UP = "sword_attack_up";
        const string SWORD_ATTACK_LEFT = "sword_attack_left";
        const string SWORD_ATTACK_RIGHT = "sword_attack_right";
    
        // Animator to control player animation by code
        Animator playerAnimator;
        // String to store current animation that is playing
        String currentState = DOWN_IDLE;
        // Boolean to check Player is attacking or not
        private bool isAttacking;
        // PlayerMovement Script Reference to constraint movement
        PlayerMovement playerMovement;
    
        void Awake()
        {
            // Getting the Animator component of player
            playerAnimator = GetComponent<Animator>();
            // Getting the PlayerMovement Script Component of player
            playerMovement = GetComponent<PlayerMovement>();
        }

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
            if (currentState == newState) return;
            
            // Statement to change the current animation to the new
            // animation based on user input
            currentState = newState;
    
            // Statement to play the animations
            // playerAnimator.Play(newState);
            playerAnimator.CrossFade(newState, normalCrossFadeTime);
        }
        
        // Function to store the logic of which "SWORD" animation to play
        // and at what time/Input
        void AnimateStateSword(string newState)
        {
            // This statement makes sure the same animation is not
            // played twice at the same time
            if (currentState == newState) return;
            
            // Statement to change the current animation to the new
            // animation based on user input
            currentState = newState;
    
            // Statement to play the animations
            playerAnimator.CrossFade(newState, swordCrossFadeTime);
        }
    
        // Function to animate player based on user input
        public void AnimatePlayer(float xDir, float yDir)
        {
            // if attack animation is playing then "return" will
            // not let the switch statement run thus locking the
            // animation at "attacking" and letting it complete before
            // "walk" animation can be played again
            if (isAttacking) return;
            // switch statements checks to play walk animations
            // First for the LEFT and RIGHT WALK animations
            switch (xDir)
            {
                case 1:
                    AnimateStateNormal(WALK_RIGHT);
                    break;
                case -1:
                    AnimateStateNormal(WALK_LEFT);
                    break;
                default:
                {
                    switch (currentState)
                    {
                        case WALK_RIGHT:
                            AnimateStateNormal(RIGHT_IDLE);
                            break;
                        case WALK_LEFT:
                            AnimateStateNormal(LEFT_IDLE);
                            break;
                    }
    
                    break;
                }
            }
    
            // Now switch checking for UP and DOWN WALK animations
            switch (yDir)
            {
                case 1:
                    AnimateStateNormal(WALK_UP);
                    break;
                case -1:
                    AnimateStateNormal(WALK_DOWN);
                    break;
                default:
                {
                    switch (currentState)
                    {
                        case WALK_UP:
                            AnimateStateNormal(UP_IDLE);
                            break;
                        case WALK_DOWN:
                            AnimateStateNormal(DOWN_IDLE);
                            break;
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
                playerMovement.Freeze();
                isAttacking = true;
                // if-else checks for UP and DOWN ATTACK animations
                if (y.Equals(-1))
                {
                    AnimateStateNormal(ATTACK_DOWN);
                }
                else if (y.Equals(1))
                {
                    AnimateStateNormal(ATTACK_UP);
                }
                else if (y.Equals(0))
                {
                    if (currentState == DOWN_IDLE) AnimateStateNormal(ATTACK_DOWN);
                    else if (currentState == UP_IDLE) AnimateStateNormal(ATTACK_UP);
                }
                
                // if-else checks for LEFT and RIGHT ATTACK animations
                if (x.Equals(-1)) AnimateStateNormal(ATTACK_LEFT);
                else if (x.Equals(1)) AnimateStateNormal(ATTACK_RIGHT);
                else if (x.Equals(0))
                {
                    if (currentState == LEFT_IDLE) AnimateStateNormal(ATTACK_LEFT);
                    else if (currentState == RIGHT_IDLE) AnimateStateNormal(ATTACK_RIGHT);
                }
                
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                // Player movement resumes while not attacking
                playerMovement.UnFreeze();
                isAttacking = false;
                // switch statement checks to play their idle animations respectively
                switch (currentState)
                {
                    case ATTACK_DOWN:
                    {
                        if (x.Equals(0))
                        {
                            AnimateStateNormal(DOWN_IDLE);
                        }

                        break;
                    }
                    case ATTACK_UP:
                    {
                        if (x.Equals(0))
                        {
                            AnimateStateNormal(UP_IDLE);
                        }

                        break;
                    }
                    case ATTACK_LEFT:
                    {
                        if (y.Equals(0))
                        {
                            AnimateStateNormal(LEFT_IDLE);
                        }

                        break;
                    }
                    case ATTACK_RIGHT:
                    {
                        if (y.Equals(0))
                        {
                            AnimateStateNormal(RIGHT_IDLE);
                        }

                        break;
                    }
                }
            }
        }

        void SwordAttack(float xd, float yd)
        {
            // Play animation at right click
            if (Input.GetMouseButtonDown(1))
            {
                // Player movement is stopped while attacking
                playerMovement.Freeze();
                isAttacking = true;
                // if-else checks for UP and DOWN ATTACK animations
                if (yd.Equals(-1))
                {
                    AnimateStateSword(SWORD_ATTACK_DOWN);
                }
                else if (yd.Equals(1))
                {
                    AnimateStateSword(SWORD_ATTACK_UP);
                }
                else if (yd.Equals(0))
                {
                    if (currentState == DOWN_IDLE) AnimateStateSword(SWORD_ATTACK_DOWN);
                    else if (currentState == UP_IDLE) AnimateStateSword(SWORD_ATTACK_UP);
                }
                
                // if-else checks for LEFT and RIGHT ATTACK animations
                if (xd.Equals(-1)) AnimateStateSword(SWORD_ATTACK_LEFT);
                else if (xd.Equals(1)) AnimateStateSword(SWORD_ATTACK_RIGHT);
                else if (xd.Equals(0))
                {
                    if (currentState == LEFT_IDLE) AnimateStateSword(SWORD_ATTACK_LEFT);
                    else if (currentState == RIGHT_IDLE) AnimateStateSword(SWORD_ATTACK_RIGHT);
                }
                
            }
            else if(Input.GetMouseButtonUp(1))
            {
                // Player movement resumes while not attacking
                playerMovement.UnFreeze();
                isAttacking = false;
                // switch statement checks to play their idle animations respectively
                switch (currentState)
                {
                    case SWORD_ATTACK_DOWN:
                    {
                        if (xd.Equals(0))
                        {
                            AnimateStateSword(DOWN_IDLE);
                        }

                        break;
                    }
                    case SWORD_ATTACK_UP:
                    {
                        if (xd.Equals(0))
                        {
                            AnimateStateSword(UP_IDLE);
                        }

                        break;
                    }
                    case SWORD_ATTACK_LEFT:
                    {
                        if (yd.Equals(0))
                        {
                            AnimateStateSword(LEFT_IDLE);
                        }

                        break;
                    }
                    case SWORD_ATTACK_RIGHT:
                    {
                        if (yd.Equals(0))
                        {
                            AnimateStateSword(RIGHT_IDLE);
                        }

                        break;
                    }
                }
            }
        }
}
