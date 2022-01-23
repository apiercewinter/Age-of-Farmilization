using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject spawnUnit(UnitScriptableObject unitType)
    {
        GameObject spawnedUnit = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
        UnitScript script = spawnedUnit.GetComponent<UnitScript>();
        script.myData = unitType;

        return spawnedUnit;
    }
}
