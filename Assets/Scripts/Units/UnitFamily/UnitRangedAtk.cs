//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRangedAtk : UnitAttacker
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private ProjectileSOTargeted projectileInfo;

    public override bool attack(RaycastHit hit)
    {
        GameObject target = hit.transform.gameObject;

        //Check if they have a health script (if not, can't be damaged/attacked).
        Health hp = target.GetComponent<Health>();
        if (!hp) return false;

        //Check if both attacking someone not on same team
        //  and if they are actually in range.
        if (gameObject.tag == target.tag) return false;
        if (!inRange(target)) return false;


        //Since in range, shoot a projectile out :)
        Vector3 projectileStart = gameObject.GetComponent<BoxCollider>().center + gameObject.transform.position;
        GameObject projectile = projectileInfo.spawnProjectile(projectilePrefab, projectileStart, Quaternion.identity);
        ProjectileTargeted p = projectile.GetComponent<ProjectileTargeted>();
        p.setDamage(getDamage());
        p.setTarget(hit);
        projectile.tag = gameObject.tag;


        return true;
    }

    public void setProjectilePrefab(GameObject pPrefab)
    {
        projectilePrefab = pPrefab;
    }

    public void setProjectileInfo(ProjectileSOTargeted pInfo)
    {
        projectileInfo = pInfo;
    }

}
