

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollector : UnitMover
{
    //This class implements some general data for collecting resources

    private float collectionMult;
    private float range;

    public override bool takeAction(RaycastHit hit)
    {
        //Check if have an action
        if (!actionAvailable) return false;

        //Use action and collect
        if (collect(hit))
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

    public virtual bool collect(RaycastHit hit)
    {
        GameObject go = hit.transform.gameObject;

        //Check if they have a resource script (if not, can't be collected).
        ResourceObject ro = go.GetComponent<ResourceObject>();
        if (!ro) return false;

        //Check if in range
        if (!inRange(go, getRange())) return false;

        //Collect !

        TeamManager.addResource(ro.resourcetype, ro.gathervalue);
        ro.DepleteResource();

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
