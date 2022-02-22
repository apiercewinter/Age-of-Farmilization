using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang
// Alec Kaxon-Rupp - Resources/Inventory

public delegate void LookAtPlayerDel(GameObject mainPlayer);

// This is a Singleton class that deals with the team, it has all the information of
// all the Teams, including its mainPlayer gameObject, units that belong to this team, etc.
//
// 2/20 UPDATE: Merged the TurnManager into the TeamManager due to the interdependency 
// between these two classes, below are the class description for the TurnManager:
// TurnManager manages the control of each team's unit
// Simply put, the turn manager changes the units of the currently controlling team to
// "Selectable" layer, and changes units of other teams to "Unselectable" layer, so the 
// player can only control units that belong to that player's team
public class TeamManager : MonoBehaviour
{
    [SerializeField]
    private GameObject unitsHolder;

    private static List<Team> teamList = new List<Team>();
    private static int currentIndex;
    private static Team currentTeam;

    private LookAtPlayerDel lookAtPlayerDel;

    [SerializeField]
    private GameObject spawnerP1;
    [SerializeField]
    private GameObject spawnerP2;

    // Start is called before the first frame update
    void Start()
    {
        instantiateTeam();
    }

    public static List<Team> getAllTeams()
    {
        return teamList;
    }

    // This method will read all the units in the two players' units holder and stored them into 
    // the currentPlayerUnits and nextPlayerUnits lists. And this should be called right after the 
    // base player units are spawned
    private void instantiateTeam()
    {
        Transform unitsParent = unitsHolder.transform;
        for (int i = 0; i < unitsParent.childCount; i++)
        {
            List<GameObject> newList = new List<GameObject>();
            Transform child = unitsParent.GetChild(i);
            GameObject mainPlayer = child.GetChild(0).gameObject;
            if (mainPlayer.tag == "AIAnimal")
            {
                newList.Add(mainPlayer);
                Team aiTeam = new Team(null, newList, mainPlayer.tag);
                teamList.Add(aiTeam);
            }
            else
            {
                Team newTeam = new Team(mainPlayer, newList, mainPlayer.tag);
                teamList.Add(newTeam);
            }
        }
        currentTeam = teamList[currentIndex];
        // After merging TurnManager into TeamManager, this method will take care
        // of giving the first player control.
        giveCurrentTeamControl();
        lookAtPlayerDel(currentTeam.getMainPlayer());
    }

    private void giveCurrentTeamControl()
    {
        foreach (GameObject go in currentTeam.getAllUnitsInList())
        {
            go.layer = LayerMask.NameToLayer("Selectable");
            go.GetComponent<UnitBase>().readyAction();
        }
        GameObject mainPlayer = currentTeam.getMainPlayer();
        mainPlayer.layer = LayerMask.NameToLayer("Selectable");
        mainPlayer.GetComponent<UnitBase>().readyAction();
    }

    private void removeCurrentTeamControl()
    {
        foreach (GameObject go in currentTeam.getAllUnitsInList())
        {
            go.layer = LayerMask.NameToLayer("Unselectable");
            go.GetComponent<UnitBase>().endTurn();
        }
        GameObject mainPlayer = currentTeam.getMainPlayer();
        mainPlayer.layer = LayerMask.NameToLayer("Unselectable");
        mainPlayer.GetComponent<UnitBase>().endTurn();
    }

    private void moveToNextIndex()
    {
        currentIndex++;
        if (currentIndex >= teamList.Count)
        {
            currentIndex = 0;
        }
        currentTeam = teamList[currentIndex];
        if (currentTeam.getTag() == "AIAnimal")
        {
            AITurn();
            currentIndex++;
            if (currentIndex >= teamList.Count)
            {
                currentIndex = 0;
            }
            currentTeam = teamList[currentIndex];
        }
    }

    public void OnNextTurnButtonClick()
    {
        removeCurrentTeamControl();
        moveToNextIndex();
        giveCurrentTeamControl();
        GetComponent<TransitionManager>().showTransitionCanvas(currentTeam.getMainPlayer().name);
        lookAtPlayerDel(currentTeam.getMainPlayer());
        
    }

    public void SwapSpawnMenus()
    {
        if (spawnerP1.activeSelf)
        {
            spawnerP1.SetActive(false);
            spawnerP2.SetActive(true);
        }
        else if (spawnerP2.activeSelf)
        {
            spawnerP2.SetActive(false);
            spawnerP1.SetActive(true);
        }
    }

    public void AITurn()
    {
        foreach (GameObject ai in currentTeam.getAllUnitsInList())
        {
            ai.GetComponent<UnitBase>().readyAction();
            ai.GetComponent<AIAnimal>().performAction();
        }
    }

    // This method will determine whether the unit belongs to a team of the current turn
    public static bool inSameTeam(GameObject unit)
    {
        return unit.tag == currentTeam.getTag();
    }

    public static string getCurrentTeamTag()
    {
        return currentTeam.getTag();
    }

    // This method will add new unit into the current team
    public static void addNewUnit(GameObject go)
    {
        // adding new unit to the current mainPlayer's list
        currentTeam.addNewUnit(go);
    }

    // This method will remove the unit from the team with the specific teamTag
    public static void removeUnit(GameObject go, string teamTag)
    {
        for (int i = 0; i < teamList.Count; i++)
        {
            if (teamList[i].getTag() == teamTag)
            {
                teamList[i].removeUnit(go);
                return;
            }
        }
    }

    public static void addResource(string resourceType, int amount)
    {
        currentTeam.addToInventory(resourceType, amount);
    }

    public static void subtractResource(string resourceType, int amount)
    {
        currentTeam.subtractFromInventory(resourceType, amount);
    }

    public static int getResourceAmount(string resourceType)
    {
        return currentTeam.getResourceAmount(resourceType);
    }

    public static void setCurrentTeamIndex(int index)
    {
        currentIndex = index;
        currentTeam = teamList[currentIndex];
    }

    public void subscribeToLookAtPlayerDel(LookAtPlayerDel del)
    {
        lookAtPlayerDel += del;
    }
}

