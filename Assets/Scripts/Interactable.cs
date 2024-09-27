using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    void Interact();
    void Move(Vector2 targetDir);
    void TakeDamage();
}
