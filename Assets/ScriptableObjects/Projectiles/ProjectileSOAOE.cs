//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AOE Projectile", menuName = "ScriptableObjects/Projectile/AOE")]
public class ProjectileSOAOE : ProjectileSOBase
{
    [SerializeField] protected float blastRadius;
    [SerializeField] protected float gravity;

    public override GameObject spawnProjectile(GameObject projPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        GameObject spawnedProjectile = createBase(projPrefab, position, rotation);

        spawnedProjectile.AddComponent<ProjectileAOE>();
        setup(spawnedProjectile);

        return spawnedProjectile;
    }

    protected override void setup(GameObject go)
    {
        base.setup(go);
        ProjectileAOE p = go.GetComponent<ProjectileAOE>();
        if (!p) return; //Check to make sure it actually has the script

        p.setBlastRadius(blastRadius);
        p.setGravity(gravity);
    }
}