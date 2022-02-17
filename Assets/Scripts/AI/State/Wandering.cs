using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : State
{
    private float activityRange;
    private float randomX;
    private float randomY;

    public Wandering(GameObject _gameObject, float activityRange)
        : base(_gameObject)
    {
        currentState = STATE.WANDERING;
        this.activityRange = activityRange;
    }

    public override void enter()
    {
        base.enter();

    }

    public override void update()
    {
        base.update();
        wander();
    }

    void wander()
    {
        if (gameObject.GetComponent<NavMeshAgent>().remainingDistance == 0)
        {
            randomX = Random.Range(-activityRange, activityRange);
            randomY = Random.Range(-activityRange, activityRange);
            Vector3 currentPos = gameObject.transform.position;
            if (currentPos.x + randomX > activityRange)
            {
                randomX = activityRange - currentPos.x;
            }
            if (currentPos.y + randomY > activityRange)
            {
                randomY = activityRange - currentPos.y;
            }
            gameObject.GetComponent<UnitScript>().moveTo(new Vector3(randomX, randomY, 0) + currentPos);
        }
    }
}
