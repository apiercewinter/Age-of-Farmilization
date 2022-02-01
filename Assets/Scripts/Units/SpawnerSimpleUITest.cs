using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpawnerSimpleUITest : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject content;
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        UnitScriptableObject[] unitTypes = spawner.GetComponent<Spawner>().spawnableUnits;

        for(uint i = 0; i < unitTypes.Length; ++i)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content.transform);
            //button.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            button.transform.Find("UnitName").GetComponent<TextMeshProUGUI>().text = unitTypes[i].name;
            button.transform.Find("FoodCost").GetComponent<TextMeshProUGUI>().text = ""+ unitTypes[i].costFood;
            button.transform.Find("StoneCost").GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].costStone;
            button.transform.Find("WoodCost").GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].costWood;

            uint localI = i;
            button.GetComponent<Button>().onClick.AddListener(() => {
                spawner.GetComponent<Spawner>().spawnUnit(localI);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
