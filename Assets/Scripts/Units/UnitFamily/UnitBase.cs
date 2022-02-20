//Aaron Winter

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
        //Blank implementation in case want to implement in future
    }

    protected virtual void Update()
    {
        //Blank implementation in case want to implement in future
    }

    // When an unit has 0 health, call this destroy() method to destroy the gameObject
    // add code here to take care of some businesses before destroying the gameObject
    public void destroy()
    {
        gameObject.layer = 0;
        TeamManager.removeUnit(gameObject, gameObject.tag);
        // deathEffect will destroy the gameObject when the "animation" is over
        Destroy(gameObject);
    }

    // The gameObject that should die will rotate in z axis to the ground over
    // "time" seconds, and then destroy. This creates a fall to ground effect
    public IEnumerator deathEffect(float time)
    {
        Transform myTransform = gameObject.transform;
        while (true)
        {
            myTransform.rotation = new Quaternion(myTransform.rotation.x, myTransform.rotation.y,
                myTransform.rotation.z + (Time.deltaTime / time), myTransform.rotation.w);
            yield return null;
            if (myTransform.rotation.z >= 0.7f)
            {
                break;
            }
        }
        Destroy(gameObject);
    }
}
