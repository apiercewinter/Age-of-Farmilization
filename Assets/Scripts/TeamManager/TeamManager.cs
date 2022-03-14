// Boyuan Huang
// Alec Kaxon-Rupp - Resources/Inventory/Spawning/Debugging

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void LookAtPlayerDel(GameObject mainPlayer);
public delegate void WinDel(string winnerTag);

// This is a Singleton class that deals with the team, it has all the information of
// all the Teams, including its mainPlayer gameObject, units that belong to this team, etc.
//
// 2/20 UPDATE: Merged the TurnManager into the TeamManager due to the interdependency 
// between these two classes, below are the old class description for the TurnManager:
//
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

    private static Action godViewDel;
    private static LookAtPlayerDel lookAtPlayerDel;
    private static WinDel winDel;

    [SerializeField]
    private GameObject spawnerP1;
    [SerializeField]
    private GameObject spawnerP2;

    [SerializeField]
    private GameObject bases;

    private static bool hasWinner = false;

    // Start is called before the first frame update
    void Start()
    {
        instantiateTeam();
        SwapSpawnMenus();
        GetComponent<TransitionManager>().subscribeToLookAtPlayerDel(lookAtCurrentPlayer);
    }

    private IEnumerator TurnRoutine() //moved NextButton code to Coroutine to help resolve conflict between UI Canvases (Daniel Zhang)
    {
        SelectedObject.deselect();
        removeCurrentTeamControl();
        moveToNextIndex();
        yield return new WaitForSeconds(0.1f);
        if (hasWinner)
            StopCoroutine("TurnRoutine");
        else
            GetComponent<TransitionManager>().showTransitionCanvas(currentTeam.getTag());
        giveCurrentTeamControl();
        godViewDel();
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
            GameObject playerBase = null;
            if (mainPlayer.tag == "AIAnimal")
            {
                for (int j = 0; j < child.childCount; j++)
                {
                    newList.Add(child.GetChild(j).gameObject);
                }
                Team aiTeam = new Team(null, playerBase, newList, mainPlayer.tag);
                teamList.Add(aiTeam);
            }
            else
            {
                foreach (Transform baseContainer in bases.transform)
                {
                    if (baseContainer.name == mainPlayer.tag)
                    {
                        playerBase = baseContainer.GetChild(0).gameObject;
                    }
                }
                Team newTeam = new Team(mainPlayer, playerBase, newList, mainPlayer.tag);
                teamList.Add(newTeam);
            }
        }
        currentTeam = teamList[teamList.Count -1];
        if (currentTeam.getTag() == "AIAnimal" || currentTeam.getTag() == "PlayerAI")
        {
            moveToNextIndex();
        }
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
        if (mainPlayer != null)
        {
            mainPlayer.layer = LayerMask.NameToLayer("Selectable");
            mainPlayer.GetComponent<UnitBase>().readyAction();
        }
    }

    private void removeCurrentTeamControl()
    {
        foreach (GameObject go in currentTeam.getAllUnitsInList())
        {
            go.layer = LayerMask.NameToLayer("Unselectable");
            go.GetComponent<UnitBase>().endTurn();
        }
        GameObject mainPlayer = currentTeam.getMainPlayer();
        if (mainPlayer != null)
        {
            mainPlayer.layer = LayerMask.NameToLayer("Unselectable");
            mainPlayer.GetComponent<UnitBase>().endTurn();
        }
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
            moveToNextIndex();
        }
        if (currentTeam.getTag() == "PlayerAI")
        {
            PlayerAITurn();
            moveToNextIndex();
        }
    }

    public void OnNextTurnButtonClick()
    {
        // a failsafe
        StartCoroutine("TurnRoutine");
    }

    public void lookAtCurrentPlayer()
    {
        GameObject mainPlayer = currentTeam.getMainPlayer();
        if (mainPlayer != null)
        {
            lookAtPlayerDel(currentTeam.getMainPlayer());
        }
    }

    public void SwapSpawnMenus()
    {
        if (currentTeam.getTag()=="Player1")
        {
            spawnerP1.SetActive(true);
            spawnerP2.SetActive(false);
        }
        else if (currentTeam.getTag()=="Player2")
        {
            spawnerP2.SetActive(true);
            spawnerP1.SetActive(false);
        }
    }

    private void AITurn()
    {
        List<GameObject> units = currentTeam.getAllUnitsInList();
        foreach (GameObject ai in units)
        {
            ai.GetComponent<UnitBase>().readyAction();
            ai.GetComponent<AIAnimal>().performAction();
        }
    }

    private void PlayerAITurn()
    {
        List<GameObject> units = currentTeam.getAllUnitsInList();
        GameObject mainPlayer = currentTeam.getMainPlayer();
        foreach (GameObject unit in units)
        {
            unit.GetComponent<UnitBase>().readyAction();
        }
        // if main player is still alive, it will give orders to its units
        if (mainPlayer)
        {
            mainPlayer.GetComponent<UnitBase>().readyAction();
            mainPlayer.GetComponent<AIMainPlayer>().performAction();
        }
        // if main player is already dead, all of its units will just start wandering
        else
        {
            foreach (GameObject unit in units)
            {
                UnitMover myMover = unit.GetComponent<UnitMover>();
                Vector3 randomDir = UnityEngine.Random.insideUnitCircle.normalized;
                myMover.moveRel(randomDir * (myMover.getMoveDistance() - 1));
            }
        }
    }

    // This method will determine whether the unit belongs to a team of the current turn
    public static bool inSameTeam(GameObject unit)
    {
        return unit.tag == currentTeam.getTag();
    }

    public static bool getHasWinner()
    {
        return hasWinner;
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

    public static void removeBase(string teamTag)
    {
        // Removes the Units of the team that had their base destroyed
        for (int i = 0; i < teamList.Count; i++)
        {
            Team team = teamList[i];
            if (team.getTag() == teamTag)
            {
                teamList.RemoveAt(i);
                team.destroyAll(Destroy);
                break;
            }
        }
     
        int teamLeft = 0;
        Team leftTeam = null;
        foreach (Team team in teamList)
        {
            if (team.getBase() != null && team.getTag().StartsWith("Player"))
            {
                leftTeam = team;
                teamLeft++;
            }
        }

        //Declares the winning team
        if (leftTeam != null)
        {
            string winnerTag = leftTeam.getTag();

            if (teamLeft == 1)
            {
                hasWinner = true;
                lookAtPlayerDel(leftTeam.getMainPlayer());
                winDel(winnerTag);
            }

        }

        
    }

    public static void addResource(string resourceType, int amount)
    {
        //Adds resources to the teams inventory
        currentTeam.addToInventory(resourceType, amount);
    }

    public static void subtractResource(string resourceType, int amount)
    {
        //Removes resources from the teams inventory
        currentTeam.subtractFromInventory(resourceType, amount);
    }

    public static int getResourceAmount(string resourceType)
    {
        //Returns the amount of a resource that a team has
        if(currentTeam!=null)
        {
            return currentTeam.getResourceAmount(resourceType);
        }

        return 0;
    }

    public static void setCurrentTeamIndex(int index)
    {
        currentIndex = index;
        currentTeam = teamList[currentIndex];
    }

    public static void subscribeToLookAtPlayerDel(LookAtPlayerDel del)
    {
        lookAtPlayerDel += del;
    }

    public static void subscribeToWinDel(WinDel del)
    {
        winDel += del;
    }

    public static void subscribeToGodViewDel(Action del)
    {
        godViewDel += del;
    }

    public static void resetAll()
    {
        //Resets all static variables when player restarts match or returns to main menu.  Needs to be done so these variables aren't pointing to null references
        teamList.Clear();
        currentIndex = 0;
        currentTeam = null;
        hasWinner = false;
        lookAtPlayerDel = null;
        winDel = null;
        godViewDel = null;
        SelectedObject.resetAll();
    }

    public static List<GameObject> getUnitsByTeamTag(string teamTag)
    {
        foreach (Team team in teamList)
        {
            if (team.getTag() == teamTag)
            {
                return team.getAllUnitsInList();
            }
        }
        return null;
    }

    public static GameObject getBaseByTeamTag(string teamTag)
    {
        foreach (Team team in teamList)
        {
            if (team.getTag() == teamTag)
            {
                return team.getBase();
            }
        }
        return null;
    }
}

