using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Writer: Boyuan Huang
public class Wandering : State
{
    private float activityRange;
    private Vector3 startingPos;

    public Wandering(GameObject _gameObject, float activityRange, Vector3 startingPos)
        : base(_gameObject)
    {
        currentState = STATE.WANDERING;
        this.activityRange = activityRange;
        this.startingPos = startingPos;
    }

    public override void enter()
    {
        base.enter();

    }

    public override void update()
    {
        base.update();
        // activityRange is the same as the moveDistance
        /*Debug.Log("in update: roundStartLocation: " + gameObject.GetComponent<UnitMover>().getRoundStartLocation());
        Debug.Log("in updaye: starting pos: " + startingPos);
        Debug.Log("in update: transform.loaction: " + gameObject.transform.position);*/
        Vector3 movement = new Vector3(Random.Range(-activityRange, activityRange), 0, Random.Range(-activityRange, activityRange)).normalized * (activityRange - 1);
        Vector3 destination = movement + gameObject.transform.position;
        // If the destination is outside of the circle the Wandering behavior should be in
        // then change the destination to a normalized vector, which represents the direction 
        // from the center of the range to the destination
        if (Vector3.Distance(destination, startingPos) > activityRange)
        {
            destination = (destination - startingPos).normalized;
            destination *= (activityRange - 1);
        }

        Debug.Log("Distacne between destination and starting location: " + Vector3.Distance(destination, startingPos));

        gameObject.GetComponent<UnitMover>().move(destination);
        //gameObject.GetComponent<UnitMover>().moveRel(movement);
    }
}
