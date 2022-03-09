// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all AI behavior children
public abstract class AI : MonoBehaviour
{
    protected State currentState;

    public virtual void performAction()
    {
        currentState.update();
    }
}
