using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public delegate void LookAtPlayerDel(GameObject mainPlayer);
public delegate void CurrentTeamUpdateDel(int currentIndex);

public class TurnManager : MonoBehaviour
{
    private List<Team> TeamOrderList = new List<Team>(); 

    public int currentIndex = 0;

    private LookAtPlayerDel lookAtPlayerDel;
    private CurrentTeamUpdateDel currentTeamUpdateDel;

    // Start is called before the first frame update
    void Start()
    {
        getTeamList();
        TeamManager.subscribeToTeamListUpdateDel(getTeamList);
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

    // Refresh the teamOrderList when there is update
    void getTeamList()
    {
        TeamOrderList = TeamManager.getAllTeams();
        Debug.Log("just refresh teamOrderList:");
        Debug.Log("team list from Turn Manager");
        foreach (GameObject go in TeamOrderList[1].getAllUnitsInList())
        {
            Debug.Log(go.name);
        }
    }
    

    void moveToNextIndex()
    {
        currentIndex++;
        if (currentIndex >= TeamOrderList.Count)
        {
            currentIndex = 0;
        }
        currentTeamUpdateDel(currentIndex);
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

    public void subscribeToCurrentTeamUpdateDel(CurrentTeamUpdateDel del)
    {
        currentTeamUpdateDel += del;
    }
}
