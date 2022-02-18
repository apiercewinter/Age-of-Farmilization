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
        this.gameObject.tag = "AI";
    }

    // Base class AI's performAction simply just calls the state to 
    // do its thing, we need to determine whether we need to change 
    // the state in the children's performAction() method.
    public virtual void performAction()
    {
        currentState.update();
    }
}
