// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AIAttacking will first starts as Wandering() within a specfic range
// As soon as there is a unit that enters its attacking range, it will chase and attack that unit
// If the unit runs out of its chasing range, AIAttacking will go to back Wandering() again.
public class AIAttacking : AIAnimal
{
    private GameObject target = null;
    [SerializeField]
    private float chasingRange = 30;
    private Vector3 startingPos;
    private HashSet<GameObject> targetSet = new HashSet<GameObject>();
    private HashSet<GameObject> chasingSet = new HashSet<GameObject>();
    private float moveDistance;

    // Start is called before the first frame update
    void Start()
    {
        moveDistance = gameObject.GetComponent<UnitMover>().getMoveDistance();
        startingPos = gameObject.transform.position;
        // Attacking animal will start as wandering when the game first starts
        currentState = new Wandering(gameObject, moveDistance, startingPos);
    }

    private bool hasInRangeTarget()
    {
        return targetSet.Count != 0;
    }

    private bool hasInChasingRangeTarget()
    {
        return chasingSet.Count != 0;
    }

    // AIAttacking will remember all the targets that are still in chasing range
    // Due to Unity's issue, HashSet.Remove(null) doesn't work, so I have to create this method below
    // to sort of remove null from the resourceSet, for more information, refer to the link below:
    // https://issuetracker.unity3d.com/issues/hashset-dot-removewhere-does-not-correctly-evaluate-null-for-gameobjects-in-builds
    // Thanks to Alec for finding this issue
    private void clearChasingSet()
    {
        HashSet<GameObject> newSet = new HashSet<GameObject>();
        foreach (GameObject go in chasingSet)
        {
            if (go != null && Vector3.Distance(startingPos, go.transform.position) < chasingRange)
            {
                newSet.Add(go);
            }
        }
        chasingSet = newSet;
    }

    private void clearTargetSet()
    {
        foreach (GameObject go in targetSet)
        {
            if (go != null && Vector3.Distance(startingPos, go.transform.position) < chasingRange &&
                Vector3.Distance(gameObject.transform.position, go.transform.position) > moveDistance)
            {
                Debug.Log(go.name);
                chasingSet.Add(go);
            }
        }
        targetSet.Clear();
    }

    protected override void refreshSet()
    {
        clearTargetSet();
        clearChasingSet();
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
        // Transition from Wandering State to Attacking State
        // Wandering -> Attacking
        if (hasInRangeTarget())
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
            currentState = new Attacking(this.gameObject, target);
        }
        // Transition from Attacking State to Chasing State
        // Attacking -> Chasing
        else if (!hasInRangeTarget() && hasInChasingRangeTarget())
        {
            // Find the closet threat
            float closetDist = Mathf.Infinity;
            foreach (GameObject threat in chasingSet)
            {
                float thisDist = Vector3.Distance(gameObject.transform.position, threat.transform.position);
                if (thisDist < closetDist)
                {
                    closetDist = thisDist;
                    target = threat;
                }
            }
            currentState = new Chasing(this.gameObject, target);
        }
        // Transition from Attacking State and Chasing State to Wandering State
        // Attacking & Chasing -> Wandering
        else
        {
            if (currentState.ToString() != "Wandering")
            {
                currentState = new Wandering(this.gameObject, moveDistance, startingPos);
            }
        }
    }

    public override void performAction()
    {
        base.performAction();
        // Debug.Log("AIAttacking current state: " + currentState.ToString());
    }
}
