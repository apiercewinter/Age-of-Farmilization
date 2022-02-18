using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Writer: Boyuan Huang
public class Wandering : State
{
    private float activityRange;
    private float randomX;
    private float randomY;
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
        wander();
    }

    void wander()
    {
        if (gameObject.GetComponent<NavMeshAgent>().remainingDistance < 2)
        {
            Debug.Log(gameObject.GetComponent<NavMeshAgent>().remainingDistance);
            randomX = Random.Range(-activityRange, activityRange);
            randomY = Random.Range(-activityRange, activityRange);
            Vector3 currentPos = gameObject.transform.position;
            if (currentPos.x + randomX > startingPos.x + activityRange)
            {
                randomX = activityRange + startingPos.x - currentPos.x;
            }
            if (currentPos.y + randomY > startingPos.y + activityRange)
            {
                randomY = activityRange + startingPos.y - currentPos.y;
            }
            gameObject.GetComponent<UnitScript>().moveTo(new Vector3(randomX, randomY, 0) + currentPos);
            Debug.Log("magnitude to the current Postion: " + Vector3.Distance(startingPos, gameObject.transform.position));
        }
    }
}
