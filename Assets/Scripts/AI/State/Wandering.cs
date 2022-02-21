using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Writer: Boyuan Huang
public class Wandering : State
{
    private float activityRange;
    private Vector3 startingPos;

    public Wandering(GameObject _gameObject, float activityRange)
        : base(_gameObject)
    {
        currentState = STATE.WANDERING;
        this.activityRange = activityRange;
        startingPos = gameObject.transform.position;
        Debug.Log("starting Position is: " + startingPos);
    }

    public override void enter()
    {
        base.enter();

    }

    public override void update()
    {
        base.update();
        Vector3 movement = new Vector3(Random.Range(-activityRange, activityRange), 0, Random.Range(-activityRange, activityRange)).normalized *
            gameObject.GetComponent<UnitMover>().getMoveDistance() + gameObject.transform.position;

        gameObject.GetComponent<UnitMover>().move(movement);

    }
}
