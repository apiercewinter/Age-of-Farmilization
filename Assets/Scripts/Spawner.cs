using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject spawnedUnit = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
        UnitScript script = spawnedUnit.GetComponent<UnitScript>();
        GameObject model = Instantiate(unitType.modelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        model.transform.SetParent(spawnedUnit.transform);
        script.myData = unitType;

        return spawnedUnit;
    }
}
