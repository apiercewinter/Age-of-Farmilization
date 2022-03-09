// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AIHarvesting animal will start as Seeking() for resource. When it encounters any unit from player,
// it will flee away from that unit. When there is no unit that is within its alert range, it will start
// its last action again. Upon finding resource, it will remember them, and collect them one by one.
public class AIHarvesting : AIAnimal
{
    private GameObject resourceToGather = null;
    private GameObject target = null;
    private HashSet<GameObject> targetSet = new HashSet<GameObject>();
    private HashSet<GameObject> resourceSet = new HashSet<GameObject>();
    private float moveDistance;

    void Start()
    {
        moveDistance = gameObject.GetComponent<UnitMover>().getMoveDistance();
        currentState = new Seeking(this.gameObject, moveDistance);
    }

    private bool isSafe()
    {
        return targetSet.Count == 0;
    }

    private bool hasResourceTarget()
    {
        return resourceSet.Count != 0;
    }

    // Because AIHarvesting will remember all the resources it found, so we don't want AIHarvesting
    // to clear its ressourceSet, we only need to remove null from it.
    // Due to Unity's issue, HashSet.Remove(null) doesn't work, so I have to create this method below
    // to sort of remove null from the resourceSet, for more information, refer to the link below:
    // https://issuetracker.unity3d.com/issues/hashset-dot-removewhere-does-not-correctly-evaluate-null-for-gameobjects-in-builds
    // Thanks to Alec for finding this issue
    private void removeNullInResourceSet()
    {
        HashSet<GameObject> newSet = new HashSet<GameObject>();
        foreach (GameObject go in resourceSet)
        {
            if (go != null)
            {
                newSet.Add(go);
            }
        }
        resourceSet = newSet;
    }

    protected override void refreshSet()
    {
        targetSet.Clear();
        // AIHarvesting animal will remember the resource they found
        removeNullInResourceSet();

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, moveDistance);

        foreach (Collider collider in colliders)
        {
            GameObject collidedGO = collider.gameObject;
            if (collidedGO.tag == "Resource")
            {
                resourceSet.Add(collidedGO);
            }
            else if (collidedGO.tag.StartsWith("Player"))
            {
                targetSet.Add(collidedGO);
            }
        }  
    }

    protected override void decideState()
    {
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
            if (currentState.ToString() != "Seeking")
            {
                currentState = new Seeking(this.gameObject, moveDistance);
            }
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
        base.performAction();
    }
}
