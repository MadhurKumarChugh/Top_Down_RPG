using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    void Interact();
    void UpAnim(float y);
    void DownAnim(float y);
    void RightAnim(float x);
    void LeftAnim(float x);
    void AttackChecker();
}
