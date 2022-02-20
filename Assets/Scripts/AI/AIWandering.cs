using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// AIWandering is just a simple AI animal that will just wander around
public class AIWandering : AI
{
    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        currentState = new Wandering(this.gameObject, 10);
    }

    protected override void removeNULL()
    {
        base.removeNULL();
    }

    protected override void decideState()
    {
        base.decideState();
    }

    // Update is called once per frame
    void Update()
    {
        performAction();
    }

    public override void performAction()
    {
        base.performAction();
    }
}
