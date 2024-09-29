using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAnimationController : MonoBehaviour
{
    private enum State
    {
        WalkDown,
        WalkUp,
        WalkLeft,
        WalkRight,
        DownIdle,
        UpIdle,
        LeftIdle,
        RightIdle,
    }
    const string Walk = "WalkDown";

    // Animator to control player animation by code
    Animator _enemyAnimator;
    // String to store current animation that is playing
    String _currentState = Walk;
    int _stt;
    
    void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
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
        _enemyAnimator.Play(newState);
    }
    
    // Function to animate enemy based on facing direction 
    public void AnimateEnemy(float x, float y, int state)
    {
        _stt = state;
        if (state == 1)
        {
            if(y < 0) AnimateStateNormal(State.WalkDown.ToString());
            else AnimateStateNormal(State.WalkUp.ToString());
        }
        else if (state == 2)
        {
            if(x < 0) AnimateStateNormal(State.WalkLeft.ToString());
            else AnimateStateNormal(State.WalkRight.ToString());
        }
        else if (state == 3)
        {
            if(_currentState == State.WalkDown.ToString()) AnimateStateNormal(State.DownIdle.ToString());
            else if (_currentState == State.WalkUp.ToString()) AnimateStateNormal(State.UpIdle.ToString());
            else if (_currentState == State.WalkLeft.ToString()) AnimateStateNormal(State.LeftIdle.ToString());
            else if (_currentState == State.WalkRight.ToString()) AnimateStateNormal(State.RightIdle.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
