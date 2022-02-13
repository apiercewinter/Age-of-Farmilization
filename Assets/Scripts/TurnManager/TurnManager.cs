using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject unitsHolder;
    private List<GameObject> currentPlayerUnits = new List<GameObject>();
    private List<GameObject> nextPlayerUnits = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        readAllUnits();
        switchSelectableLayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            switchSelectableLayer();
        }
    }

    // This method will read all the units in the two players' units holder and stored them into 
    // the currentPlayerUnits and nextPlayerUnits lists. And this should be called right after the 
    // base player units are spawned
    void readAllUnits()
    {
        Transform onePlayerUnits = unitsHolder.transform.GetChild(0);
        Transform anotherPlayerUnits = unitsHolder.transform.GetChild(1);
        foreach (Transform go in onePlayerUnits)
        {
            currentPlayerUnits.Add(go.gameObject);
        }
        foreach (Transform go in anotherPlayerUnits)
        {
            nextPlayerUnits.Add(go.gameObject);
        }
    }

    // This method will change the layer of all the gameObjects of the current player that is playing 
    // to "Unselectable", and change the layer of all the gameObjects of the next player to "Selectable"
    void switchSelectableLayer()
    {
        foreach (GameObject go in currentPlayerUnits)
        {
            go.layer = LayerMask.NameToLayer("Selectable");
        }
        foreach (GameObject go in nextPlayerUnits)
        {
            go.layer = LayerMask.NameToLayer("Unselectable");
        }
        List<GameObject> tmpList = currentPlayerUnits;
        currentPlayerUnits = nextPlayerUnits;
        nextPlayerUnits = tmpList;
    }
}
