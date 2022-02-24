//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : UnitCollector
{
    //The player unit, can collect and (melee) attack.
    //  Uses same range for attacking and collecting

    [SerializeField] private float damage;

    public override bool takeAction(GameObject go, Vector3 pos = new Vector3())
    {
        //Check if have an action
        if (!actionAvailable) return false;

        //Use action and try to attack then try to collect
        if (attack(go, pos) || collect(go))
        {
            stop();
            actionAvailable = false;
            return true;
        }
        else
        { // failed both
            return false;
        }
    }

    public virtual bool inRange(GameObject target)
    {
        return inRange(target, getRange());
    }

    public void setDamage(float d)
    {
        damage = d;
    }
    public float getDamage()
    {
        return damage;
    }

    public bool attack(GameObject go, Vector3 pos = new Vector3())
    {
        //Check if they have a health script (if not, can't be damaged/attacked).
        Health hp = go.GetComponent<Health>();
        if (!hp) return false;

        //Check if both attacking someone not on same team
        //  and if they are actually in range.
        if (gameObject.tag == go.tag) return false;
        if (!inRange(go)) return false;


        //Since in range, just do damage (no projectiles)
        hp.Damage(getDamage());
        return true;
    }
}
