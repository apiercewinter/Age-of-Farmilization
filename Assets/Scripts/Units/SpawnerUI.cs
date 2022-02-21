//Aaron Winter
//Alec Kaxon-Rupp
//Daniel Zhang

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SpawnerUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject content;
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        UnitSOBase[] unitTypes = spawner.GetComponent<UnitSpawner>().getSpawnableUnits();

        for (uint i = 0; i < unitTypes.Length; ++i)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content.transform);

            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unitTypes[i].name;
            button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Food");
            button.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Stone");
            button.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Wood");
            button.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Gold");
            button.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Silver");

            uint localI = i;
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                spawner.GetComponent<UnitSpawner>().spawnUnit(localI);
            });
        }
    }

}
