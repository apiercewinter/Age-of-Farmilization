using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public class AIHarvesting : AI
{
    private GameObject resourceToGather = null;
    private GameObject target = null;
    private State lastState;
    private bool lastTargetStillInRange = false;
    private HashSet<GameObject> targetSet = new HashSet<GameObject>();
    private HashSet<GameObject> resourceSet = new HashSet<GameObject>();

    void Start()
    {
        this.tag = "AIAnimal";
        currentState = new Seeking(this.gameObject, 10);
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.transform.parent = gameObject.transform;
        boxCollider.size = new Vector3(30, 5, 30);
        boxCollider.isTrigger = true;
        lastState = null;
    }

    // Update is called once per frame
    void Update()
    {
        performAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.tag == "Resource")
        {
            resourceSet.Add(otherGO);
        }
        else if (otherGO.tag.StartsWith("Player"))
        {
            targetSet.Add(otherGO);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.tag.StartsWith("Player"))
        {
            if (targetSet.Contains(otherGO))
            {
                targetSet.Remove(otherGO);
            }
        }
    }

    private bool isSafe()
    {
        return targetSet.Count == 0;
    }

    private bool hasResourceTarget()
    {
        return resourceSet.Count != 0;
    }

    public void decideState()
    {
        targetSet.Remove(null);
        resourceSet.Remove(null);
        // Transition from Seeking State to Harvesting State
        // Seeking & Fleeing -> Harvesting
        if (isSafe() && currentState.ToString() != "Harvesting" && hasResourceTarget())
        {
            if (resourceToGather == null)
            {
                float closetDist = Mathf.Infinity;
                foreach (GameObject resource in resourceSet)
                {
                    float thisDist = Vector3.Distance(gameObject.transform.position, resource.transform.position);
                    if (thisDist < closetDist)
                    {
                        closetDist = thisDist;
                        resourceToGather = resource;
                    }
                }
            }
            currentState = new Harvesting(this.gameObject, resourceToGather);
        }
        // Transition from Harvesting State or Fleeing State to Seeking State
        // Harvesting & Fleeing -> Seeking
        else if (isSafe() && !hasResourceTarget())
        {
            // If the current state is already Seeking, we can actually just return
            if (currentState.ToString() == "Seeking")
            {
                return;
            }
            currentState = new Seeking(this.gameObject, 10);
        }
        // Transition from any States to Fleeing State
        // Fleeing & Harvesting & Seeking -> Fleeing
        else if (!isSafe())
        {
            // Find the closet threat
            float closetDist = Mathf.Infinity;
            foreach (GameObject threat in targetSet)
            {
                float thisDist = Vector3.Distance(gameObject.transform.position, threat.transform.position);
                if (thisDist < closetDist)
                {
                    closetDist = thisDist;
                    target = threat;
                }
            }
            currentState = new Fleeing(this.gameObject, target);
        }
    }

    public override void performAction()
    {
        decideState();
        base.performAction();
        //Debug.Log(currentState.ToString());
    }
}
