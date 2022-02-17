using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFleeing : AI
{
    private GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        // Fleeing animal will start as wandering when the game first starts
        currentState = new Wandering(gameObject, 10);
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = gameObject.transform.position;
        boxCollider.size = new Vector3(15, 15, 15);
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
        if (otherGO.gameObject.tag != "AI" && otherGO.gameObject.tag != "AIAnimal")
        {
            target = otherGO;
            currentState = new Fleeing(gameObject, target);
        }
    }

    public override void performAction()
    {
        // The Fleeing animal will start wandering again if the enemy is 15 away
        if (target != null && Vector3.Distance(target.transform.position, gameObject.transform.position) > 15)
        {
            currentState = new Wandering(gameObject, 10);
        }
        base.performAction();
    }
}
