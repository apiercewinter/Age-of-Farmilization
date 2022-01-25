using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpawnerSimpleUITest : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject canvas;
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        UnitScriptableObject[] unitTypes = spawner.GetComponent<Spawner>().spawnableUnits;

        for(uint i = 0; i < unitTypes.Length; ++i)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(canvas.transform);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector3(((int)i-4)*55, 0, 0);
            button.GetComponentInChildren<TextMeshProUGUI>().text = unitTypes[i].name;

            uint localI = i;
            button.GetComponent<Button>().onClick.AddListener(() => {
                spawner.GetComponent<Spawner>().spawnUnit(localI, new Vector3());
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
