//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitSOBase : ScriptableObject
{
    [SerializeField] protected GameObject modelPrefab;

    [SerializeField] protected string unitName;
    [SerializeField] protected string unitType;

    [SerializeField] protected float maxHealth;

    [SerializeField] protected int costFood;
    [SerializeField] protected int costStone;
    [SerializeField] protected int costWood;
    [SerializeField] protected int costSilver;
    [SerializeField] protected int costGold;

    //Just creates the unit, doesn't set things like tags for teams...
    public abstract GameObject spawnUnit(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform teamContainer = null);
    
    // Call this before other spawnUnit bodies.
    protected GameObject createBase(GameObject unitPrefab, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform teamContainer = null)
    {
        GameObject spawnedUnit;
        if(teamContainer != null)
            spawnedUnit = Instantiate(unitPrefab, position, rotation, teamContainer);
        else
            spawnedUnit = Instantiate(unitPrefab, position, rotation);

        spawnedUnit.name = unitName;
        Health health = spawnedUnit.GetComponent<Health>();
        if (health) health.setMaxHealth(maxHealth);

        //Set Model
        GameObject model = Instantiate(modelPrefab);
        model.transform.SetParent(spawnedUnit.transform, false);

        return spawnedUnit;
    }



    //Returns the cost of a resource. Negative values indicate
    //  invalid resource.
    public int getCost(string resource)
    {
        //Not possible to have a serialized field dictionary in Unity (that you can change in editor), 
        //  so using switch case with constant and finite amount of resources
        switch (resource)
        {
            case "Food":
                return costFood;
            case "Stone":
                return costStone;
            case "Wood":
                return costWood;
            case "Silver":
                return costSilver;
            case "Gold":
                return costGold;
            default: //Dummy implementation - resource doesn't exist
                break;
        }

        return -1;
    }
}
