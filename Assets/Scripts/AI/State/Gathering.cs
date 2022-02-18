using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering : State
{
    private GameObject resourceToGather;

    public Gathering(GameObject _gameObject, GameObject resource)
        : base(_gameObject)
    {
        resourceToGather = resource;
    }

    public override void update()
    {
        base.update();
        if (resourceToGather != null)
        {
            gameObject.GetComponent<UnitScript>().gather(resourceToGather);
        }
    }
}
