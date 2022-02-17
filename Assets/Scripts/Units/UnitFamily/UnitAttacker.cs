using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAttacker : UnitMover
{

    //This class implements some general data for attacking units, does not have an implemented,
    //  attack, and thus cannot be used alone.

    private float damage;
    private float range;

    public override bool takeAction(RaycastHit hit)
    {
        //Check if have an action
        if (!actionAvailable) return false;

        //Use action and attack
        if(attack(hit))
        {
            actionAvailable = false;
            return true;
        }
        else
        { // failed attack
            return false;
        }
    }

    public abstract bool attack(RaycastHit hit);

    public void setDamage(float d)
    {
        damage = d;
    }
    public float getDamage()
    {
        return damage;
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
