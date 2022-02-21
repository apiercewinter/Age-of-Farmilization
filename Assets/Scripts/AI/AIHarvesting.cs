using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// AIHarvesting animal will start as Seeking() for resource. When it encounters any unit from player,
// it will flee away from that unit. When there is no unit that is within its alert range, it will start
// its last action again. Upon finding resource, it will remember them, and collect them one by one.
public class AIHarvesting : AI
{
    private GameObject resourceToGather = null;
    private GameObject target = null;
    private HashSet<GameObject> targetSet = new HashSet<GameObject>();
    private HashSet<GameObject> resourceSet = new HashSet<GameObject>();
    private float moveDistance;

    void Start()
    {
        this.tag = "AIAnimal";
        moveDistance = gameObject.GetComponent<UnitMover>().getMoveDistance();
        currentState = new Seeking(this.gameObject, moveDistance);
        /*BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.transform.parent = gameObject.transform;
        boxCollider.size = new Vector3(30, 30, 30);
        boxCollider.isTrigger = true;*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnTriggerEnter(Collider other)
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
    }*/

    private bool isSafe()
    {
        return targetSet.Count == 0;
    }

    private bool hasResourceTarget()
    {
        return resourceSet.Count != 0;
    }

    // For some reason I don't know, the HashSet does not remove null,
    // so I had to create this method to sort of remove null from it
    private void removeNullInResourceSet()
    {
        string res = "new resourceSet: ";
        HashSet<GameObject> newSet = new HashSet<GameObject>();
        foreach (GameObject go in resourceSet)
        {
            if (go != null)
            {
                newSet.Add(go);
                res += go.name;
            }
        }
        resourceSet = newSet;
        Debug.Log(res);
    }

    protected override void refreshSet()
    {
        // AIHarvesting animal will remember the resource they found
        targetSet.Clear();

        // =====================================================
        /*string resourcesss = "remove only go resouce set: ";
        foreach (GameObject go in resourceSet)
        {
            if (go != null)
            {
                resourcesss += go.name;
            }
            else
            {
                resourcesss += "null";
            }
        }
        Debug.Log(resourcesss);
        string resourcess = "before removing resouce set: ";
        foreach (GameObject go in resourceSet)
        {
            if (go != null)
            {
                resourcess += go.name;
            }
            else
            {
                resourcess += "null";
            }
        }
        Debug.Log(resourcess);*/
        // ======================================================

        removeNullInResourceSet();

        /*string resources = "before collidering resouce set: ";
        foreach (GameObject go in resourceSet)
        {
            if (go != null)
            {
                resources += go.name;
            }
            else
            {
                resources += "null";
            }
        }
        Debug.Log(resources);*/ 

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, moveDistance);

        string co = "Collider list: ";

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
            co += collidedGO.name + ", ";
        }
        Debug.Log(co);

        Debug.Log("just refreshed list, new resourceSetlength: " + resourceSet.Count);
        string resourceset = "resouce set: ";
        foreach (GameObject go in resourceSet)
        {
            if (go != null)
            {
                resourceset += go.name;
            }
            else
            {
                resourceset += "null";
            }
        }
        Debug.Log(resourceset);
    }

    protected override void decideState()
    {
        /*string resourceStr = "Resources: ";
        foreach (GameObject go in resourceSet)
        {
            resourceStr += go.name + ", ";
        }
        string targetStr = "Targets: ";
        foreach (GameObject go in targetSet)
        {
            targetStr += go.name + ", ";
        }
        Debug.Log(resourceStr);
        Debug.Log(targetStr);*/
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
            Debug.Log("become seeking");
            // If the current state is already Seeking, we can actually just return
            if (currentState.ToString() == "Seeking")
            {
                return;
            }
            currentState = new Seeking(this.gameObject, moveDistance);
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
        Debug.Log("AIHarvesting's state: " + currentState.ToString());
        //Debug.Log(currentState.ToString());
    }
}
