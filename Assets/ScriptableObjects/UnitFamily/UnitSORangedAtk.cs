//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Attacker", menuName = "ScriptableObjects/Units/Attacker/Ranged")]
public class UnitSORangedAtk : UnitSOAttacker
{
    //Handles all the ranged attacker parameter setting
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected ProjectileScriptableObject projectileInfo;

    //To be called after creating the UnitRangedAttacker component
    public void setupRangedAttacker(GameObject go)
    {
        setupAttacker(go);
        UnitRangedAtk us = go.GetComponent<UnitRangedAtk>();
        if (!us) return; //Check to make sure it actually has the script

        us.setProjectilePrefab(projectilePrefab);
        us.setProjectileInfo(projectileInfo);
    }

    public override GameObject spawnUnit(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform teamContainer = null)
    {
        GameObject spawnedUnit = createBase(unitPrefab, position, rotation, teamContainer);

        spawnedUnit.AddComponent<UnitRangedAtk>();
        setupRangedAttacker(spawnedUnit);

        return spawnedUnit;
    }
}