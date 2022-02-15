using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public delegate void LookAtPlayerDel(GameObject mainPlayer);

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject unitsHolder;

    private List<Team> TeamOrderList = new List<Team>(); 

    private int currentIndex = 0;

    private LookAtPlayerDel lookAtPlayerDel;

    // Start is called before the first frame update
    void Start()
    {
        instantiateTeam();
        switchSelectableLayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            switchControl();
        }
    }

    // This method will read all the units in the two players' units holder and stored them into 
    // the currentPlayerUnits and nextPlayerUnits lists. And this should be called right after the 
    // base player units are spawned
    void instantiateTeam()
    {
        Transform unitsParent = unitsHolder.transform;
        for (int i = 0; i < unitsParent.childCount; i++)
        {
            List<GameObject> newList = new List<GameObject>();
            Transform child = unitsParent.GetChild(i);
            Team newTeam = new Team(child.GetChild(0).gameObject, newList);
            TeamOrderList.Add(newTeam);
        }
    }

    void moveToNextIndex()
    {
        currentIndex++;
        if (currentIndex >= TeamOrderList.Count)
        {
            currentIndex = 0;
        }
    }    

    // This method will change the layer of all the gameObjects of the current player that is playing 
    // to "Unselectable", and change the layer of all the gameObjects of the next player to "Selectable"
    void switchSelectableLayer()
    {
        foreach (GameObject go in TeamOrderList[currentIndex].getAllUnitsInList())
        {
            go.layer = LayerMask.NameToLayer("Unselectable");
        }
        TeamOrderList[currentIndex].getMainPlayer().layer = LayerMask.NameToLayer("Unselectable");
        moveToNextIndex();
        foreach (GameObject go in TeamOrderList[currentIndex].getAllUnitsInList())
        {
            go.layer = LayerMask.NameToLayer("Selectable");
        }
        TeamOrderList[currentIndex].getMainPlayer().layer = LayerMask.NameToLayer("Selectable");
    }

    void lookAtCurrentPlayer()
    {

        lookAtPlayerDel(TeamOrderList[currentIndex].getMainPlayer());
    }

    void switchControl()
    {
        // This two methods will switch current to next, next to current
        switchSelectableLayer();
        lookAtCurrentPlayer();
        GetComponent<TransitionManager>().showTransitionCanvas(TeamOrderList[currentIndex].getMainPlayer().name);
    }

    public void subscribeToLookAtPlayerDel(LookAtPlayerDel del)
    {
        lookAtPlayerDel += del;
    }
}
