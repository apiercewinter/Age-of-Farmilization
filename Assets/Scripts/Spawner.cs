using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;
    public UnitScriptableObject example;

    // Start is called before the first frame update
    void Start()
    {
        spawnUnit(example);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject spawnUnit(UnitScriptableObject unitType)
    {
        //Instantiate Unit, get UnitScript & Health
        GameObject spawnedUnit = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
        UnitScript script = spawnedUnit.GetComponent<UnitScript>();

        //Add UnitScriptableObject (raw data) to the unit
        script.myData = unitType;

        //Add a model to the Unit (based off of unitType prefab)
        GameObject model = Instantiate(unitType.modelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        script.setModel(model);


        return spawnedUnit;
    }
}
