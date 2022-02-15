//Aaron Winter
//Alec Kaxon-Rupp
//Daniel Zhang


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
    public uint maxSpawnDistanceFromPlayer = 150;

    private GameObject myTeamContainer;
    private GameObject myPlayer;
    private ResourceScript Inventory;
    public GameObject InventoryDisplayObject;
    private ResourcesDisplay Display;

    // Start is called before the first frame update
    void Start()
    {
        myTeamContainer = new GameObject(teamUnitContainerPrefix + team);
        if(unitContainer != null)
        { //Put the teamContainer under this general Unit container
            myTeamContainer.transform.SetParent(unitContainer.transform, true);
        }

        Inventory = myTeamContainer.AddComponent<ResourceScript>();
        try
        {
            Display = InventoryDisplayObject.GetComponent<ResourcesDisplay>();
            Display.Inventory = Inventory;
            Inventory.Display = Display;
        }
        catch { }

        myPlayer = spawnUnit(playerUnit, gameObject.transform.position, gameObject.transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Tries to spawn the unit specified by unitIndex from the Spawner's spawnableUnits[].
    //  Fails (and returns null) if the unitIndex is invalid
    public GameObject spawnUnit(uint unitIndex, Vector3 position, Quaternion rotation = new Quaternion())
    {
        if (unitIndex >= spawnableUnits.Length) return null;
        if (!myPlayer) return null;
        if (Vector3.Distance(position, myPlayer.transform.position) > maxSpawnDistanceFromPlayer) return null;
        UnitScriptableObject unitType = spawnableUnits[unitIndex];

        //Not enough resources to spawn
        if (Inventory.GetResourceAmount("Food") < unitType.costFood ||
            Inventory.GetResourceAmount("Stone") < unitType.costStone ||
            Inventory.GetResourceAmount("Wood") < unitType.costWood ||
            Inventory.GetResourceAmount("Silver") < unitType.costSilver ||
            Inventory.GetResourceAmount("Gold") < unitType.costGold) return null;

        //Take away resources
        Inventory.SubtractResourceAmount("Food", unitType.costFood);
        Inventory.SubtractResourceAmount("Stone", unitType.costStone);
        Inventory.SubtractResourceAmount("Wood", unitType.costWood);
        Inventory.SubtractResourceAmount("Silver", unitType.costSilver);
        Inventory.SubtractResourceAmount("Gold", unitType.costGold);
        GameObject spawnedUnit = spawnUnit(spawnableUnits[unitIndex], position, rotation);
        spawnedUnit.tag = TeamManager.getCurrentTeamTag();
        TeamManager.addNewUnit(spawnedUnit);
        return spawnedUnit;
    }

    //Spawn on mouse when accessed through unit menu
    public GameObject spawnUnit(uint unitIndex, Transform spawnPos)
    {
        Debug.Log("Yes" + spawnPos.position);
        return spawnUnit(unitIndex, spawnPos.position, spawnPos.rotation);
    }

    private GameObject spawnUnit(UnitScriptableObject unitType, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        //Instantiate Unit, get UnitScript & Health
        GameObject spawnedUnit = Instantiate(unitPrefab, position, rotation, myTeamContainer.transform);
        spawnedUnit.name = unitType.unitName;
        spawnedUnit.tag = team;
        UnitScript script = spawnedUnit.GetComponent<UnitScript>();

        //Add UnitScriptableObject (raw data) & team to the Unit
        script.unitData = unitType;
        script.team = team;
        script.Inventory = Inventory;
        return spawnedUnit;
    }

}
