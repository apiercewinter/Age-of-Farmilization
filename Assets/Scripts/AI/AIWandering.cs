using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// AIWandering is just a simple AI animal that will just wander around
public class AIWandering : AI
{
    private float moveDistance;
    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        moveDistance = gameObject.GetComponent<UnitMover>().getMoveDistance();
        currentState = new Wandering(this.gameObject, moveDistance);
    }

    protected override void refreshSet()
    {
        base.refreshSet();
    }

    protected override void decideState()
    {
        base.decideState();
    }

    public override void performAction()
    {
        base.performAction();
    }
}
