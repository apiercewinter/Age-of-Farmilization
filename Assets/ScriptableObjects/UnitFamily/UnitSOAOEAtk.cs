//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOT ACTUALLY IMPLEMENTED
[CreateAssetMenu(fileName = "New AOE Attacker", menuName = "ScriptableObjects/Units/Attacker/AOE")]
public class UnitSOAOEAtk : UnitSOAttacker
{
    //Handles all the aoe attacker parameter setting
    //[SerializeField] protected GameObject projectilePrefab;
    //[SerializeField] protected ProjectileScriptableObject projectileInfo;

    //To be called after creating the UnitaoeAttacker component
    public void setupAOEAttacker(GameObject go)
    {
        setupAttacker(go);
        UnitAOEAtk us = go.GetComponent<UnitAOEAtk>();
        if (!us) return; //Check to make sure it actually has the script

        //us.setProjectilePrefab(projectilePrefab);
        //us.setProjectileInfo(projectileInfo);
    }

    public override GameObject spawnUnit(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform teamContainer = null)
    {
        GameObject spawnedUnit = createBase(unitPrefab, position, rotation, teamContainer);

        spawnedUnit.AddComponent<UnitAOEAtk>();
        setupAOEAttacker(spawnedUnit);

        return spawnedUnit;
    }
}