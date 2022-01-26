using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITesting : MonoBehaviour
{

    public float activityRange = 10.0f;
    public float randomX;
    public float randomY;
    public float idleTime = 2f;
    public bool isMoving = false;
    public bool waited = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveThenIdle());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isMoveing: " + isMoving + ", remainingDistance: " + GetComponent<NavMeshAgent>().remainingDistance);
    }

    IEnumerator MoveThenIdle()
    {
        while (true)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();

            if (agent.remainingDistance == 0)
            {
                randomX = Random.Range(-activityRange, activityRange);
                randomY = Random.Range(-activityRange, activityRange);
                Vector3 destination = new Vector3(randomX, randomY, 0) + transform.position;

                GetComponent<NavMeshAgent>().SetDestination(destination);

                isMoving = true;
            }

            Debug.Log("Waiting for " + idleTime + " secs");

            yield return new WaitForSeconds(idleTime);

            Debug.Log("IdleTime finished, move to next destination");

    }
    }
}
