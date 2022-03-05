// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AIWandering is just a simple AI animal that will just wander around
public class AIWandering : AIAnimal
{
    private float moveDistance;
    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        moveDistance = gameObject.GetComponent<UnitMover>().getMoveDistance();
        startingPos = gameObject.transform.position;
        currentState = new Wandering(this.gameObject, moveDistance, startingPos);
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
