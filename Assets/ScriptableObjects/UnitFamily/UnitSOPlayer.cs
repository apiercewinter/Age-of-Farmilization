//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObjects/Units/Player")]
public class UnitSOPlayer : UnitSOCollector
{
    [SerializeField] protected float damage;

    //To be called after creating the player component
    public void setupPlayer(GameObject go)
    {
        setupCollector(go);
        UnitPlayer us = go.GetComponent<UnitPlayer>();
        if (!us) return; //Check to make sure it actually has the script

        us.setDamage(damage);
    }

    public override GameObject spawnUnit(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform teamContainer = null)
    {
        GameObject spawnedUnit = createBase(unitPrefab, position, rotation, teamContainer);

        spawnedUnit.AddComponent<UnitPlayer>();
        setupPlayer(spawnedUnit);

        return spawnedUnit;
    }
}