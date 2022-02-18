//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitMover : UnitBase
{
    //This class implements moving for other units, does not have an action,
    //  and thus cannot be used alone.

    //Can move a maximum of moveDistance each round
    private float moveDistance;
    protected Vector3 roundStartLocation;

    private NavMeshAgent myAgent;
    public const float AGENT_SPEED = 10f;
    public const float AGENT_ACC = 100f;
    public const float AGENT_TURN = 100f;
    public const float AGENT_STOP_DIST = .5f;

    public override void readyAction()
    {
        base.readyAction();
        roundStartLocation = transform.position;
    }

    //Returns whether you can move there (in case out of range or don't have action).
    public virtual bool move(Vector3 loc)
    {
        if (!actionAvailable) return false;
        if (Vector3.Distance(roundStartLocation, loc) > moveDistance) return false;

        //Movement code here :)
        myAgent.SetDestination(loc);

        return true;
    }

    public virtual void stop()
    { //Full stops. No smooth acceleration. To be used when an action is taken, for instance.
        myAgent.ResetPath();
        myAgent.velocity = Vector3.zero;
    }

    public void setMoveDistance(float m)
    {
        moveDistance = m;
    }

    public float getMoveDistance()
    {
        return moveDistance;
    }


    protected override void Start()
    {
        base.Start();
        roundStartLocation = transform.position;
        
        //NavMeshAgent settings
        myAgent = gameObject.GetComponent<NavMeshAgent>();
        myAgent.speed = AGENT_SPEED;
        myAgent.acceleration = AGENT_ACC;
        myAgent.angularSpeed = AGENT_TURN;
        myAgent.stoppingDistance = AGENT_STOP_DIST;


    }

    protected override void Update()
    {
        base.Update();
    }
}
