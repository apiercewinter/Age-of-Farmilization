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

    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        currentState = new Seeking(this.gameObject, 10); // I put 10 here because I still do not have movement range implemented
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = gameObject.transform.position;
        boxCollider.size = new Vector3(30, 30, 30);
    }

    // Update is called once per frame
    void Update()
    {
        performAction();
    }

    /*private GameObject findTarget()
    {
        GameObject possibleTarget = null;
        float minDist = Mathf.Infinity;
        foreach (Transform teamContainer in unitsContainer)
        {
            if (teamContainer.childCount > 0)
            {
                Transform childMainPlayer = teamContainer.transform.GetChild(0);
                string childMainPlayerTag = childMainPlayer.tag;
                float distance = Vector3.Distance(this.gameObject.transform.position, childMainPlayer.position);
                // Barbarian will find the closest main player and set it as its target
                if (childMainPlayerTag != "AI" && childMainPlayerTag != "AIAnimal" && distance < minDist)
                {
                    minDist = distance;
                    possibleTarget = childMainPlayer.gameObject;
                }
            }
        }
        return possibleTarget;
    }*/

    public override void performAction()
    {
        if (target == null && currentState.ToString() == "Attacking")
        {
            currentState = new Seeking(this.gameObject, 10);
        }
        base.performAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.gameObject.tag != "AIAnimal")
        {
            target = otherGO;
            currentState = new Attacking(this.gameObject, target);
        }
    }
}
