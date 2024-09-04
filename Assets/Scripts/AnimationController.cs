using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
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

    // Animator to control player animation by code
    Animator playerAnimator;
    // String to store current animation that is playing
    private String currentState = DOWN_IDLE;

    void Awake()
    {
        // Getting the Animator component of the player
        playerAnimator = GetComponent<Animator>();
    }

    // Function to store the logic of which animation to play
    // and at what time/Input
    public void AnimateState(string newState)
    {
        // This statement makes sure the same animation is not
        // played twice at the same time
        if (currentState == newState) return;

        // Statement to play the animations
        playerAnimator.Play(newState);

        // Statement to change the current animation to the new
        // animation based on user input
        currentState = newState;
    }

    // Function to animate player based on user input
    public void AnimatePlayer(float x, float y)
    {
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
