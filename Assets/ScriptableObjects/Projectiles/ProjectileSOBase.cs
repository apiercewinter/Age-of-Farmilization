//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileSOBase : ScriptableObject
{
    [SerializeField] protected GameObject modelPrefab;

    [SerializeField] protected string projectileName;

    [SerializeField] protected float timeInAir;

    //Just creates the unit, doesn't set things like tags for teams...
    public abstract GameObject spawnProjectile(GameObject projPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion());

    // Call this before other spawnUnit bodies.
    protected GameObject createBase(GameObject projPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        GameObject spawnedProjectile;
        spawnedProjectile = Instantiate(projPrefab, position, rotation);

        spawnedProjectile.name = projectileName;

        //Set Model
        GameObject model = Instantiate(modelPrefab);
        model.transform.SetParent(spawnedProjectile.transform, false);
        //Animator animator = model.GetComponent<Animator>();
        //if (animator) animator.logWarnings = false;

        return spawnedProjectile;
    }

    protected virtual void setup(GameObject go)
    {
        ProjectileBase p = go.GetComponent<ProjectileBase>();
        if (!p) return; //Check to make sure it actually has the script

        p.setTimeInAir(timeInAir);
    }
}
