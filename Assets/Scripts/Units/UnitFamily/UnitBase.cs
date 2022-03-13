//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    protected bool actionAvailable = false;
    protected Animator myAnimator;

    //Returns whether the action went through.
    public abstract bool takeAction(GameObject go, Vector3 pos = new Vector3());

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
        //Set animator
        myAnimator = gameObject.GetComponentInChildren<Animator>();
        if (myAnimator)
        {
            myAnimator.logWarnings = false;
        }
    }

    protected virtual void Update()
    {
        //Do animations
        if(myAnimator)
        {
            animate();
        }
    }

    //Can assume myAnimator is set
    protected virtual void animate()
    {
        //Blank implementation so can be added to update without needing
        // to implement
    }

    // When an unit has 0 health, call this destroy() method to destroy the gameObject
    // add code here to take care of some businesses before destroying the gameObject
    public void destroy()
    {
        gameObject.layer = 0;
        TeamManager.removeUnit(gameObject, gameObject.tag);
        Destroy(gameObject);
    }
}
