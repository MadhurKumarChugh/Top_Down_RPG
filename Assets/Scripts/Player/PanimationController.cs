using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanimationController : MonoBehaviour
{
     // Animation State Names In The Animator Window of Unity
        const string DOWN_IDLE = "down_idle";
        const string UP_IDLE = "up_idle";
        const string LEFT_IDLE = "left_idle";
        const string RIGHT_IDLE = "right_idle";
        const string WALK_DOWN = "walk_down";
        const string WALK_UP = "walk_up";
        const string WALK_LEFT = "walk_left";
        const string WALK_RIGHT = "walk_right";
        const string ATTACK_DOWN = "attack_down";
        const string ATTACK_UP = "attack_up";
        const string ATTACK_LEFT = "attack_left";
        const string ATTACK_RIGHT = "attack_right";
    
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
        public void AttackCheck(float x, float y)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Player movement is stopped while attacking
                playerMovement.Freeze();
                isAttacking = true;
                // if-else checks for UP and DOWN ATTACK animations
                if (y.Equals(-1))
                {
                    AnimateState(ATTACK_DOWN);
                }
                else if (y.Equals(1))
                {
                    AnimateState(ATTACK_UP);
                }
                else if (y.Equals(0))
                {
                    if (currentState == DOWN_IDLE) AnimateState(ATTACK_DOWN);
                    else if (currentState == UP_IDLE) AnimateState(ATTACK_UP);
                }
                
                // if-else checks for LEFT and RIGHT ATTACK animations
                if (x.Equals(-1)) AnimateState(ATTACK_LEFT);
                else if (x.Equals(1)) AnimateState(ATTACK_RIGHT);
                else if (x.Equals(0))
                {
                    if (currentState == LEFT_IDLE) AnimateState(ATTACK_LEFT);
                    else if (currentState == RIGHT_IDLE) AnimateState(ATTACK_RIGHT);
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
                            AnimateState(DOWN_IDLE);
                        }

                        break;
                    }
                    case ATTACK_UP:
                    {
                        if (x.Equals(0))
                        {
                            AnimateState(UP_IDLE);
                        }

                        break;
                    }
                    case ATTACK_LEFT:
                    {
                        if (y.Equals(0))
                        {
                            AnimateState(LEFT_IDLE);
                        }

                        break;
                    }
                    case ATTACK_RIGHT:
                    {
                        if (y.Equals(0))
                        {
                            AnimateState(RIGHT_IDLE);
                        }

                        break;
                    }
                }
            }
        }
    
        // Function to store the logic of which animation to play
        // and at what time/Input
        void AnimateState(string newState)
        {
            // This statement makes sure the same animation is not
            // played twice at the same time
            if (currentState == newState) return;
            
            // Statement to change the current animation to the new
            // animation based on user input
            currentState = newState;
    
            // Statement to play the animations
            // playerAnimator.Play(newState);
            playerAnimator.CrossFade(newState, 0.2f);
        }
    
        // Function to animate player based on user input
        public void AnimatePlayer(float x, float y)
        {
            // if attack animation is playing then "return" will
            // not let the switch statement run thus locking the
            // animation at "attacking" and letting it complete before
            // "walk" animation can be played again
            if (isAttacking) return;
            // switch statements checks to play walk animations
            // First for the LEFT and RIGHT WALK animations
            switch (x)
            {
                case 1:
                    AnimateState(WALK_RIGHT);
                    break;
                case -1:
                    AnimateState(WALK_LEFT);
                    break;
                default:
                {
                    switch (currentState)
                    {
                        case WALK_RIGHT:
                            AnimateState(RIGHT_IDLE);
                            break;
                        case WALK_LEFT:
                            AnimateState(LEFT_IDLE);
                            break;
                    }
    
                    break;
                }
            }
    
            // Now switch checking for UP and DOWN WALK animations
            switch (y)
            {
                case 1:
                    AnimateState(WALK_UP);
                    break;
                case -1:
                    AnimateState(WALK_DOWN);
                    break;
                default:
                {
                    switch (currentState)
                    {
                        case WALK_UP:
                            AnimateState(UP_IDLE);
                            break;
                        case WALK_DOWN:
                            AnimateState(DOWN_IDLE);
                            break;
                    }
    
                    break;
                }
            }
        }
}
