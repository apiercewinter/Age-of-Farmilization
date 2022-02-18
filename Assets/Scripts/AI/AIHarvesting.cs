using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public class AIHarvesting : AI
{
    private GameObject resourceToGather = null;

    void Start()
    {
        this.tag = "AIAnimal";
        currentState = new Seeking(this.gameObject, 10);
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = gameObject.transform.position;
        boxCollider.size = new Vector3(30, 30, 30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.tag == "Resource")
        {
            resourceToGather = otherGO;
        }
    }
}
