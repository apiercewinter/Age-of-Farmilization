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

    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        // Attacking animal will start as wandering when the game first starts
        currentState = new Wandering(gameObject, 10);
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = gameObject.transform.position;
        boxCollider.size = new Vector3(attackingRange, attackingRange, attackingRange);
        startingPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current state is: " + currentState.ToString());
        performAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.gameObject.tag != "AIAnimal")
        {
            target = otherGO;
            currentState = new Attacking(gameObject, target);
        }
    }

    public override void performAction()
    {
        // The Attacking animal will go back to wander after the target is out of its chasing range
        if (target != null && Vector3.Distance(target.transform.position, startingPos) > chasingRange)
        {
            currentState = new Wandering(gameObject, 10);
        }
        base.performAction();
    }
}
