//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Attacker", menuName = "ScriptableObjects/Units/Attacker/Melee")]
public class UnitSOMeleeAtk : UnitSOAttacker
{
    public override GameObject spawnUnit(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform teamContainer = null)
    {
        GameObject spawnedUnit = createBase(unitPrefab, position, rotation, teamContainer);

        spawnedUnit.AddComponent<UnitMeleeAtk>();
        setupAttacker(spawnedUnit);

        return spawnedUnit;
    }
}