using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// Base class for all AI behavior children
public abstract class AI : MonoBehaviour
{
    protected State currentState;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "PlayerAI";
    }

    // Remove any null object inthe set in the child's variable
    protected virtual void removeNULL()
    {
        return;
    }

    // Decide what state the AIAnimal should be in
    // This is the method that makes all the transition
    // in a FSM into code
    protected virtual void decideState()
    {
        return;
    }

    // Base class AI's performAction simply just calls removeNULL(), decideState()
    // method. The child class should decide what currentState is when it is this
    // AIAnimal's turn.
    public virtual void performAction()
    {
        removeNULL();
        decideState();
        currentState.update();
    }
}
