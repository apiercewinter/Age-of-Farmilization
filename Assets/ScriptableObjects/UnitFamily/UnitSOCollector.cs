//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collector", menuName = "ScriptableObjects/Units/Collector")]
public class UnitSOCollector : UnitSOMover
{
    //Handles all the collector parameter setting
    [SerializeField] protected float collectionMultiplier;
    [SerializeField] protected float range;

    //To be called after creating the healer component
    public void setupCollector(GameObject go)
    {
        setupMovement(go);
        UnitCollector us = go.GetComponent<UnitCollector>();
        if (!us) return; //Check to make sure it actually has the script

        us.setCollectionMult(collectionMultiplier);
        us.setRange(range);
    }

    public override GameObject spawnUnit(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform teamContainer = null)
    {
        GameObject spawnedUnit = createBase(unitPrefab, position, rotation, teamContainer);

        spawnedUnit.AddComponent<UnitCollector>();
        setupCollector(spawnedUnit);

        return spawnedUnit;
    }
}