//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMeleeAtk : UnitAttacker
{
    public override bool attack(GameObject go, Vector3 pos = new Vector3())
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
