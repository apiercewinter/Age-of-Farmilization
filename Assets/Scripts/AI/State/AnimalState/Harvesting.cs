// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Harvesting is a state that the gameObject will just go and harvest the resource;
public class Harvesting : State
{
    private GameObject resourceToGather;

    public Harvesting(GameObject _gameObject, GameObject resource)
        : base(_gameObject)
    {
        currentState = STATE.HARVESTING;
        resourceToGather = resource;
    }

    public override void update()
    {
        base.update();
        if (resourceToGather != null)
        {
            gameObject.GetComponent<UnitCollector>().takeAction(resourceToGather);
        }
    }
}
