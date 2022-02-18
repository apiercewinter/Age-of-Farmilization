//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAOEAtk : UnitAttacker
{
    public override bool attack(RaycastHit hit)
    {
        Vector3 point = hit.point;

        //Check if point in range
        if (Vector3.Distance(point, transform.position) > getRange()) return false;

        //Since in range, shoot (aoe) projectile

        return true;
    }
}
