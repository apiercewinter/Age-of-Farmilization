// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AIFleeing will first starts as Wandering() within a specfic range
// As soon as there is a unit that enters its range, the AIFleeing will flee away from that unit
// After the AIFleeing is 15 distance away from that unit, AIFleeing will go back to Wandering()
public class AIFleeing : AIAnimal
{
    private GameObject target = null;
    private HashSet<GameObject> targetSet = new HashSet<GameObject>();
    private float moveDistance;
    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        moveDistance = gameObject.GetComponent<UnitMover>().getMoveDistance();
        startingPos = gameObject.transform.position;
        // Fleeing animal will start as wandering when the game first starts
        currentState = new Wandering(this.gameObject, moveDistance, startingPos);
    }

    private bool isSafe()
    {
        return targetSet.Count == 0;
    }

    protected override void refreshSet()
    {
        targetSet.Clear();
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, moveDistance);

        foreach (Collider collider in colliders)
        {
            GameObject collidedGO = collider.gameObject;
            if (collidedGO.tag.StartsWith("Player"))
            {
                targetSet.Add(collidedGO);
            }
        }
    }

    protected override void decideState()
    {
        // Transition from any states to Wandering State
        // Wandering or Fleeing -> Wandeirng
        if (isSafe())
        {
            if (currentState.ToString() != "Wandering")
            {
                currentState = new Wandering(this.gameObject, moveDistance, startingPos);
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
        // Debug.Log("AIFleeing's current state: " + currentState.ToString());
    }
}
