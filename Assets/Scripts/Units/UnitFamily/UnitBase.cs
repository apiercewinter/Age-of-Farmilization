using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    protected bool actionAvailable = false;

    //Returns whether the action went through.
    public abstract bool takeAction(RaycastHit hit);

    public bool canTakeAction()
    {
        return actionAvailable;
    }

    //To be used at the beginning of a turn
    public virtual void readyAction()
    {
        actionAvailable = true;
    }

    public virtual void endTurn()
    {
        actionAvailable = false;
    }

    //Most units may want one of these, so in base class
    public virtual bool inRange(GameObject target, float range)
    {
        //First create a sphere around the unit and see if it overlaps with the target in some way
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        for (uint i = 0; i < colliders.Length; ++i)
        {
            //If the unit's range overlaps with a collider on the target, then in range
            if (colliders[i].gameObject == target) return true;
        }

        //Check if not in range but transform is close (no collider possibly)
        if (Vector3.Distance(target.transform.position, transform.position) < range) return true;

        //Failed other checks
        return false;
    }

    protected virtual void Start()
    {
        //By default, cannot take action.
        actionAvailable = false;

    }

    protected virtual void Update()
    {
        //Blank implementation in case want to implement in future
    }
}
