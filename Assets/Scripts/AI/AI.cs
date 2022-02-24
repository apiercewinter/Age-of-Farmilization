// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all AI behavior children
public abstract class AI : MonoBehaviour
{
    protected State currentState;

    /*// Refresh the set every turn and get all new units that is within range in the new turn
    protected virtual void refreshSet()
    {
        return;
    }

    // Decide what state the AIAnimal should be in
    // This is the method that makes all the transition
    // in a FSM into code
    protected virtual void decideState()
    {
        return;
    }*/

    // Base class AI's performAction simply just calls removeNULL(), decideState()
    // method. The child class should decide what currentState is when it is this
    // AIAnimal's turn.
    public virtual void performAction()
    {
        currentState.update();
    }
}
