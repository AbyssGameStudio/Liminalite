using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExaminable
{
    void Examine();
    void Leave();
}
public interface IInteractable
{
    void Interact();
}

public interface ITakeable
{
    void Take();
}

public interface IUsable
{
    void Use();
}