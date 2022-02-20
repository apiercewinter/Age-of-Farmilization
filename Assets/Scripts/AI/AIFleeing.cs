using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// AIFleeing will first starts as Wandering() within a specfic range
// As soon as there is a unit that enters its range, the AIFleeing will flee away from that unit
// After the AIFleeing is 15 distance away from that unit, AIFleeing will go back to Wandering()
public class AIFleeing : AI
{
    private GameObject target = null;
    private HashSet<GameObject> targetSet = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        // Fleeing animal will start as wandering when the game first starts
        currentState = new Wandering(this.gameObject, 10);
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.transform.parent = gameObject.transform;
        boxCollider.size = new Vector3(30, 30, 30);
        boxCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("current state is: " + currentState.ToString());
        performAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.tag.StartsWith("Player"))
        {
            targetSet.Add(otherGO);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (targetSet.Contains(otherGO))
        {
            targetSet.Remove(otherGO);
        }
    }

    private bool isSafe()
    {
        return targetSet.Count == 0;
    }

    protected override void removeNULL()
    {
        targetSet.Remove(null);
        base.removeNULL();
    }

    protected override void decideState()
    {
        // Transition from any states to Wandering State
        // Wandering or Fleeing -> Wandeirng
        if (isSafe())
        {
            if (currentState.ToString() != "Wandering")
            {
                currentState = new Wandering(this.gameObject, 10);
            }
        }
        // Trasition from Wandering State to Fleeing State
        // Wandering -> Fleeing
        else
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
