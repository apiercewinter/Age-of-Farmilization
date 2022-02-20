using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Writer: Boyuan Huang

// AIBarbarian will start as Seeking() state which allows barbarians to walk all around the world
// As soon as AIBarbarian find a unit (doesn't matter whether it belongs to AI team or player team)
// Barbarian will chase and attack the target until it is dead and start Seeking() again.
public class AIBarbarian : AI
{
    private GameObject target = null;
    private HashSet<GameObject> targetSet = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        currentState = new Seeking(this.gameObject, 10); // I put 10 here because we still do not have movement range implemented
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.transform.parent = gameObject.transform;
        boxCollider.size = new Vector3(30, 30, 30);
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
        if (otherGO.tag.StartsWith("Player"))
        {
            targetSet.Add(otherGO);
        }
    }

    private bool hasTarget()
    {
        return target != null;
    }

    private bool hasTargetInSet()
    {
        return targetSet.Count != 0;
    }

    protected override void removeNULL()
    {
        targetSet.Remove(null);
        base.removeNULL();
    }

    protected override void decideState()
    {
        // Transition from Attacking State to Attacking State (This actually seems a bit redundant)
        // Attacking -> Attacking
        if (hasTarget())
        {
            currentState = new Attacking(this.gameObject, target);
        }
        // Transition from any states to Attacking State
        // Attacking (last target is null) & Seeking -> Attacking
        else if (hasTargetInSet())
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
        // Transition from any states to Seeking State
        // Attacking (no matter whether last unit is null or not) -> Seeking
        else
        {
            currentState = new Seeking(this.gameObject, 10);
        }
    }

    public override void performAction()
    {
        base.performAction();
    }
}
