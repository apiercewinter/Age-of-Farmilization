using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



//Spawner will handle all unit spawning for one team.
public class Spawner : MonoBehaviour
{
    const string teamUnitContainerPrefix = "Team_";

    public GameObject unitContainer = null; //Am empty GameObject in the Hierarchy, only used for containing these spawned units.
    public GameObject unitPrefab;
    public UnitScriptableObject playerUnit;
    public UnitScriptableObject[] spawnableUnits;
    public string team;
    public uint maxSpawnDistanceFromPlayer = 10;

    private GameObject myTeamContainer;
    private GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myTeamContainer = new GameObject(teamUnitContainerPrefix + team);
        if(unitContainer != null)
        { //Put the teamContainer under this general Unit container
            myTeamContainer.transform.SetParent(unitContainer.transform, true);
        }

        myPlayer = spawnUnit(playerUnit, gameObject.transform.position, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Tries to spawn the unit specified by unitIndex from the Spawner's spawnableUnits[].
    //  Fails (and returns null) if the unitIndex is invalid, or the position is out of range of the player.
    public GameObject spawnUnit(uint unitIndex, Vector3 position, Quaternion rotation = new Quaternion())
    {
        if (unitIndex >= spawnableUnits.Length) return null;
        if (!myPlayer) return null;
        if (Vector3.Distance(position, myPlayer.transform.position) > maxSpawnDistanceFromPlayer) return null;

        return spawnUnit(spawnableUnits[unitIndex], position, rotation);
    }

    private GameObject spawnUnit(UnitScriptableObject unitType, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        //Instantiate Unit, get UnitScript & Health
        GameObject spawnedUnit = Instantiate(unitPrefab, position, rotation, myTeamContainer.transform);
        spawnedUnit.name = unitType.unitName;
        UnitScript script = spawnedUnit.GetComponent<UnitScript>();

        //Add UnitScriptableObject (raw data) & team to the Unit
        script.unitData = unitType;
        script.team = team;

        return spawnedUnit;
    }
}
