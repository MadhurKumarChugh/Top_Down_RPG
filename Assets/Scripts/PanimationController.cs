using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanimationController : MonoBehaviour
{
    [Header("ANIMATION CROSSFADE SETTINGS")]
    [SerializeField] float normalCrossFadeTime = 0.2f;
    
    // Enum to store Animation State Names In The Animator Window of Unity
    private enum State
    {
        DownIdle,
        UpIdle,
        RightIdle,
        LeftIdle,
        WalkDown,
        WalkUp,
        WalkRight,
        WalkLeft,
    }
    const string DOWN_IDLE = "DownIdle";
    // Animator to control player animation by code
    Animator playerAnimator;
    // String to store current animation that is playing
    private String currentState = DOWN_IDLE;

    private void Awake()
    {
        // Getting the Animator component of the player
        playerAnimator = GetComponent<Animator>();
    }

    // Function to store the logic of which animation to play
    // and at what time/Input
    public void AnimateStateNormal(string newState)
    {
        // This statement makes sure the same animation is not
        // played twice at the same time
        if (currentState == newState) return;
        
        // Statement to change the current animation to the new
        // animation based on user input
        currentState = newState;
        
        // Statement to play the animations
        playerAnimator.CrossFade(newState, normalCrossFadeTime);
    }

    // Function to animate player based on user input
    public void AnimatePlayer(float xDir, float yDir)
    {
        if(yDir.Equals(1)) {AnimateStateNormal(State.WalkUp.ToString());}
        else if(yDir.Equals(-1)) {AnimateStateNormal(State.WalkDown.ToString());}
        else if(xDir.Equals(1)) {AnimateStateNormal(State.WalkRight.ToString());}
        else if(xDir.Equals(-1)) {AnimateStateNormal(State.WalkLeft.ToString());}
        else
        {
            if(currentState == State.WalkUp.ToString()) AnimateStateNormal(State.UpIdle.ToString());
            else if(currentState == State.WalkDown.ToString()) AnimateStateNormal(State.DownIdle.ToString());
            else if(currentState == State.WalkRight.ToString()) AnimateStateNormal(State.RightIdle.ToString());
            else if(currentState == State.WalkLeft.ToString()) AnimateStateNormal(State.LeftIdle.ToString());
        }
    }
    
}
