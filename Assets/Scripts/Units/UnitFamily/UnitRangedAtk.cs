using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRangedAtk : UnitAttacker
{
    public override bool attack(RaycastHit hit)
    {
        GameObject go = hit.transform.gameObject;

        //Check if they have a health script (if not, can't be damaged/attacked).
        Health hp = go.GetComponent<Health>();
        if (!hp) return false;

        //Check if both attacking someone not on same team
        //  and if they are actually in range.
        if (gameObject.tag == go.tag) return false;
        //Replace this distance with like a bounding box or something !!!!!!!!!!!!!!!!!!!!!!!!!!
        if (Vector3.Distance(go.transform.position, transform.position) > getRange()) return false;


        //Since in range, shoot a projectile out :)

        return true;
    }
}
