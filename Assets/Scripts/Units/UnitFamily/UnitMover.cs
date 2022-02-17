using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitMover : UnitBase
{
    //This class implements moving for other units, does not have an action,
    //  and thus cannot be used alone.

    //Can move a maximum of moveDistance each round
    private float moveDistance;
    protected Vector3 roundStartLocation;

    public override void readyAction()
    {
        base.readyAction();
        roundStartLocation = transform.position;
    }

    //Returns whether you can move there (in case out of range).
    public virtual bool move(Vector3 loc)
    {
        if (Vector3.Distance(roundStartLocation, loc) > moveDistance) return false;

        //Movement code here :)


        return true;
    }

    public void setMoveDistance(float m)
    {
        moveDistance = m;
    }

    public float getMoveDistance()
    {
        return moveDistance;
    }


}
