//Aaron Winter
//Alec Kaxon-Rupp
//Daniel Zhang

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollector : UnitMover
{
    //This class implements some general data for collecting resources

    [SerializeField] private float collectionMult;
    [SerializeField] private float range;

    private UIUnitCentralPublisher myPublisher;

    protected override void Start()
    {
        base.Start();
        myPublisher = GetComponent<UIUnitCentralPublisher>();
    }

    public override bool takeAction(GameObject go, Vector3 pos = new Vector3())
    {
        //Check if have an action
        if (!actionAvailable) return false;

        //Use action and collect
        if (collect(go))
        {
            stop();
            actionAvailable = false;
            return true;
        }
        else
        { // failed attack
            return false;
        }
    }

    public virtual bool collect(GameObject go)
    {
        //Check if they have a resource script (if not, can't be collected).
        ResourceObject ro = go.GetComponent<ResourceObject>();
        if (!ro) return false;

        //Check if in range
        if (!inRange(go, getRange())) return false;

        //Collect !

        TeamManager.addResource(ro.getResourcename(), ro.getGatherValue());
        ro.DepleteResource();

        if (!myPublisher)
        {//If the publisher isnt assigned, try to add one
            myPublisher = GetComponent<UIUnitCentralPublisher>();
        }
        if (myPublisher)
        {//Check if have publisher then send message
            myPublisher.indicateGatheredResource(ro.getResourcename(), ro.getGatherValue());
        }

        return true;
    }

    public void setCollectionMult(float cm)
    {
        collectionMult = cm;
    }
    public float setCollectionMult()
    {
        return collectionMult;
    }
    public void setRange(float r)
    {
        range = r;
    }
    public float getRange()
    {
        return range;
    }
}
