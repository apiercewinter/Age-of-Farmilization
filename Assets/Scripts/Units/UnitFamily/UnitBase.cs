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
}
