//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealer : UnitMover
{
    //The opposite of an attacker.

    private float healAmt;
    private float range;

    public override bool takeAction(RaycastHit hit)
    {
        //Check if have an action
        if (!actionAvailable) return false;

        GameObject go = hit.transform.gameObject;

        //Use action and heal
        if (heal(hit))
        {
            stop();
            actionAvailable = false;
            return true;
        }
        else
        { // couldn't heal
            return false;
        }
    }

    public virtual bool heal(RaycastHit hit)
    {
        GameObject target = hit.transform.gameObject;

        //Check if they have a health script (if not, can't be healed).
        Health hp = target.GetComponent<Health>();
        if (!hp) return false;

        //Check if both healing someone on same team
        //  and if they are actually in range.
        if (gameObject.tag != target.tag) return false;
        if (!inRange(target)) return false;

        //Since in range, heal
        hp.Heal(getHealAmt());
        return true;
    }

    public virtual bool inRange(GameObject target)
    {
        return inRange(target, getRange());
    }

    public void setHealAmt(float h)
    {
        healAmt = h;
    }
    public float getHealAmt()
    {
        return healAmt;
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
