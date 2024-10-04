using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAnimationController : MonoBehaviour
{
    private enum State
    {
        WalkLeft,
        WalkRight,
        LeftIdle,
        RightIdle
    }

    const string Walk = "WalkLeft";

    // Animator to control player animation by code
    Animator _enemyAnimator;
    EnemyPath _path;

    // String to store current animation that is playing
    String _currentState = Walk;
    private float _movX;

    void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _path = GetComponent<EnemyPath>();
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
    public void AnimateEnemy(int state)
    {
        if(_path.IsChasing) return;
        if (state == 0)
        {
            if (_movX <= 0)
            {
                AnimateStateNormal(State.WalkLeft.ToString());
             
            }
            else if (_movX >= 0)
            {
                AnimateStateNormal(State.WalkRight.ToString());
              
            }
        }
        else
        {
            AnimateIdle();
        }
    }


    void AnimateIdle()
    {
        if (_currentState == State.WalkLeft.ToString())
        {
            AnimateStateNormal(State.LeftIdle.ToString());
            
        }
        else if (_currentState == State.WalkRight.ToString())
        {
            AnimateStateNormal(State.RightIdle.ToString());
        }
    }

    public void AnimateChase(float x)
    {
        if (x <= 0)
        {
            AnimateStateNormal(State.WalkLeft.ToString());
        }
        else if (x >= 0)
        {
            AnimateStateNormal(State.WalkRight.ToString());
        }
    }

    void DontWalkIntoWall()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GetVectors(float x)
    {
        _movX = x;
    }
}