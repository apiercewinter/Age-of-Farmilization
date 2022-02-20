using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// AIAttacking will first starts as Wandering() within a specfic range
// As soon as there is a unit that enters its attacking range, it will chase and attack that unit
// If the unit runs out of its chasing range, AIAttacking will go to back Wandering() again.
public class AIAttacking : AI
{
    private GameObject target = null;
    [SerializeField]
    private float attackingRange = 15;
    [SerializeField]
    private float chasingRange = 30;
    private Vector3 startingPos;
    private HashSet<GameObject> targetSet = new HashSet<GameObject>();
    private HashSet<GameObject> chasingSet = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        // Attacking animal will start as wandering when the game first starts
        currentState = new Wandering(gameObject, 10);
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.transform.parent = gameObject.transform;
        boxCollider.size = new Vector3(attackingRange, attackingRange, attackingRange);
        boxCollider.isTrigger = true;
        startingPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
            // Add this game object to the chasing set first, and we will come around
            // later and determine whether it is within chasing range in decideState()
            chasingSet.Add(otherGO);
        }
    }

    private bool hasInRangeTarget()
    {
        return targetSet.Count != 0;
    }

    private bool hasInChasingRangeTarget()
    {
        return chasingSet.Count != 0;
    }

    private void removeOutsideOfChasingRangeTarget()
    {
        foreach (GameObject target in chasingSet)
        {
            if (Vector3.Distance(startingPos, target.transform.position) > chasingRange)
            {
                chasingSet.Remove(target);
            }
        }
    }

    protected override void removeNULL()
    {
        targetSet.Remove(null);
        chasingSet.Remove(null);
        removeOutsideOfChasingRangeTarget();
        base.removeNULL();
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
        // Transition from Attacking State to (Chasing State)
        // Attacking -> (Chasing)
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
            currentState = new Attacking(this.gameObject, target);
        }
        // Transition from Attacking State (and Chasing State) to Wandering State
        // Attacking (& Chasing) -> Wandering
        else
        {
            if (currentState.ToString() == "Wandering")
            {
                return;
            }
            currentState = new Wandering(this.gameObject, 10);
        }
    }

    public override void performAction()
    {
        base.performAction();
    }
}
