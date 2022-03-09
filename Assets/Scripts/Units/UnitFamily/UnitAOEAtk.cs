//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAOEAtk : UnitAttacker
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private ProjectileSOAOE projectileInfo;

    public override bool attack(GameObject go, Vector3 pos)
    {
        //Check if point in range
        if (Vector3.Distance(pos, transform.position) > getRange()) return false;

        //Since in range, shoot (aoe) projectile
        Vector3 projectileStart = gameObject.GetComponent<BoxCollider>().center + gameObject.transform.position;
        GameObject projectile = projectileInfo.spawnProjectile(projectilePrefab, projectileStart, Quaternion.identity);
        ProjectileAOE p = projectile.GetComponent<ProjectileAOE>();
        p.setDamage(getDamage());
        p.setTarget(go, pos);
        projectile.tag = gameObject.tag;

        return true;
    }

    public void setProjectilePrefab(GameObject pPrefab)
    {
        projectilePrefab = pPrefab;
    }

    public void setProjectileInfo(ProjectileSOAOE pInfo)
    {
        projectileInfo = pInfo;
    }

}
