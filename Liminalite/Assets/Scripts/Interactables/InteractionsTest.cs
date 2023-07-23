using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsTest : MonoBehaviour, IExaminable, ITakeable
{
    public void Take()
    {
        Debug.Log("Taken");
    }

    public void Examine()
    {
        Debug.Log("Examining, press F to use");
    }

    public void Leave()
    {
        Debug.Log("Leaving");
    }
}
