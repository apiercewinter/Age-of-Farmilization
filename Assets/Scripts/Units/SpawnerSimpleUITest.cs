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

    [SerializeField] private GameObject FoodCost;
    [SerializeField] private GameObject StoneCost;
    [SerializeField] private GameObject WoodCost;
    [SerializeField] private GameObject UnitName;

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
            button.transform.Find("FoodCost").GetComponent<TextMeshProUGUI>().text = ""+ unitTypes[i].GetFoodCost();
            button.transform.Find("StoneCost").GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].GetStoneCost();
            button.transform.Find("WoodCost").GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].GetWoodCost();

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
