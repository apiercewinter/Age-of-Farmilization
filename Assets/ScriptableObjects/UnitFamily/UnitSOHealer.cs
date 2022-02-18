using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healer", menuName = "ScriptableObjects/Units/Healer")]
public class UnitSOHealer : UnitSOMover
{
    //Handles all the healer parameter setting
    [SerializeField] protected float healAmt;
    [SerializeField] protected float range;

    //To be called after creating the healer component
    public void setupHealer(GameObject go)
    {
        setupMovement(go);
        UnitHealer us = go.GetComponent<UnitHealer>();
        if (!us) return; //Check to make sure it actually has the script

        us.setHealAmt(healAmt);
        us.setRange(range);
    }

    public override GameObject spawnUnit(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform teamContainer = null)
    {
        GameObject spawnedUnit = createBase(unitPrefab, position, rotation, teamContainer);

        spawnedUnit.AddComponent<UnitHealer>();
        setupHealer(spawnedUnit);

        return spawnedUnit;
    }
}