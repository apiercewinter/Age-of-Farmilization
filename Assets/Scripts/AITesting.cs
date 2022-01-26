using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITesting : MonoBehaviour
{

    public float activityRange = 10.0f;
    public float randomX;
    public float randomY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent.remainingDistance == 0)
        {
            randomX = Random.Range(-activityRange, activityRange);
            randomY = Random.Range(-activityRange, activityRange);
            Vector3 destination = new Vector3(randomX, randomY, 0) + transform.position;

            agent.SetDestination(destination);
        }
    }
}
