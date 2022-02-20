//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Targeted Projectile", menuName = "ScriptableObjects/Projectile/Targeted")]
public class ProjectileSOTargeted : ProjectileSOBase
{
    public override GameObject spawnProjectile(GameObject projPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        GameObject spawnedProjectile = createBase(projPrefab, position, rotation);

        spawnedProjectile.AddComponent<ProjectileTargeted>();
        setup(spawnedProjectile);

        return spawnedProjectile;
    }
}