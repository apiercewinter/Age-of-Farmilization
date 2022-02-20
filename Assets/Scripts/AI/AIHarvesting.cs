using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public class AIHarvesting : AI
{
    private GameObject resourceToGather = null;
    private GameObject target = null;

    void Start()
    {
        this.tag = "AIAnimal";
        currentState = new Seeking(this.gameObject, 10);
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.transform.parent = gameObject.transform;
        boxCollider.size = new Vector3(30, 5, 30);
        boxCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        performAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        // If some resource objects comes into the AIHarvesting's range, it will just start to gather one resource
        if (resourceToGather == null && otherGO.tag == "Resource")
        {
            resourceToGather = otherGO;
            currentState = new Harvesting(this.gameObject, resourceToGather);
        }
        // If some team units comes into AIHarvesting's range while it is gathering resource, it will just flee
        // otherGO.tag != "Resource" && otherGO.tag !="AIAnimal"
        if (otherGO.tag.StartsWith("Player") && currentState.ToString() == "Harvesting")
        {
            Debug.Log("this theif's fleeing method is called");
            target = otherGO;
            currentState = new Fleeing(this.gameObject, target);
        }
    }

    public override void performAction()
    {
        // If the target is destroyed while AIHarvesting is still fleeing
        // AIHarvesting will just restart harvesting if the resource is still there
        if (currentState.ToString() == "Fleeing" && target == null)
        {
            if (resourceToGather == null)
            {
                currentState = new Seeking(this.gameObject, 10);
            }
            else
            {
                currentState = new Harvesting(this.gameObject, resourceToGather);
            }
        }
        // Else if the AIHarvesing has been 15 away from the target
        // AIHarvesting will just restart harvesting if the resource is still there
        else if (currentState.ToString() == "Fleeing" && Vector3.Distance(this.gameObject.transform.position, target.transform.position) > 15)
        {
            if (resourceToGather == null)
            {
                currentState = new Seeking(this.gameObject, 10);
            }
            else
            {
                currentState = new Harvesting(this.gameObject, resourceToGather);
            }
        }
        // If the resource is depleted, AIHarvesting will starting seeking new resources
        if (currentState.ToString() == "Harvesting" && resourceToGather == null)
        {
            currentState = new Seeking(this.gameObject, 10);
        }
        base.performAction();
        //Debug.Log(currentState.ToString());
    }

    private void OnMouseOver()
    {
        Debug.Log("hovering over");
    }

    private void OnMouseExit()
    {
        Debug.Log("hovering out");
    }
}
