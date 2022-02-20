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

    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform spawnLoc;
    [HideInInspector] private bool switch1 = false;
    [HideInInspector] private bool switch2 = false;

    private IEnumerator Confirm()
    {
        switch1 = true; //Rough means of making the code work for now, most likely need to write another class to help support coroutine check; DZ

        while (!switch2)
            yield return null;

        switch1 = false;
        spawnLoc.position = findMousePosition();

        switch2 = false;
    }

    // Start is called before the first frame update
    void Start()
    {   
        UnitSOBase[] unitTypes = spawner.GetComponent<UnitSpawner>().getSpawnableUnits();

        for(uint i = 0; i < unitTypes.Length; ++i)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(content.transform);

            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unitTypes[i].name;
            button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ""+ unitTypes[i].getCost("Food");
            button.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Stone");
            button.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Wood");
            button.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Gold");
            button.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text = "" + unitTypes[i].getCost("Silver");

            uint localI = i;
            button.GetComponent<Button>().onClick.AddListener(() => {
                StartCoroutine("Confirm"); //Waits for the player to click again
                spawner.GetComponent<UnitSpawner>().spawnUnit(localI, spawnLoc);
            });
        }
    }

    private Vector3 findMousePosition()
    {
        Vector3 mousePos = new Vector3();

        RaycastHit hit;
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            mousePos = hit.point;
            Debug.Log(mousePos);
        }

        return mousePos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && switch1)
            switch2 = true;
    }
}
