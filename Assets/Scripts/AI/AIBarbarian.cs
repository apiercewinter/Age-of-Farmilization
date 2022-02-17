using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBarbarian : AI
{
    [SerializeField]
    private GameObject target;
    public Transform unitsContainer;
    public bool performed = false;


    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        // "Units" object in the Hierarchy
        unitsContainer = this.transform.parent.parent;
        target = findTarget();
        currentState = new Seeking(this.gameObject, target);
    }

    // Update is called once per frame
    void Update()
    {
        if (!performed && target != null)
        {
            performAction();
            performed = true;
        }
    }

    // This method should only be used inside this class and varies between differnt AI classes
    // Barbarians will find the cloeset main player as its target
    private GameObject findTarget()
    {
        GameObject possibleTarget = null;
        float minDist = Mathf.Infinity;
        foreach (Transform teamContainer in unitsContainer)
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
        Debug.Log(possibleTarget.name);
        return possibleTarget;
    }

    public override void performAction()
    {
        if (GetComponent<NavMeshAgent>().remainingDistance == 0 && target != null)
        {
            findTarget();
            currentState = new Attacking(this.gameObject, target);
        }
        base.performAction();
    }
}
