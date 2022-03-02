// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimal : AI
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "AIAnimal";
    }

    // Refresh the set every turn and get all new units that is within range in the new turn
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
    }

    public override void performAction()
    {
        refreshSet();
        decideState();
        base.performAction();
    }

}
