using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public delegate void TeamListUpdateDel();

// This is a Singleton class that deals with the team
public class TeamManager : MonoBehaviour
{
    [SerializeField]
    private GameObject unitsHolder;

    [SerializeField]
    private GameObject TurnManager;

    private static List<Team> teamList = new List<Team>();
    private static int currentIndex;

    private static TeamListUpdateDel teamListUpdateDel;

    // Start is called before the first frame update
    void Start()
    {
        instantiateTeam();
        TurnManager.GetComponent<TurnManager>().subscribeToCurrentTeamUpdateDel(setCurrentTeamIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Team newTeam = new Team(mainPlayer, newList, mainPlayer.tag);
            teamList.Add(newTeam);
        }
    }

    // This method will determine whether the unit belongs to a team of the current turn
    public static bool inSameTeam(GameObject unit)
    {
        return unit.tag == teamList[currentIndex].getTag();
    }

    public static string getCurrentTeamTag()
    {
        return teamList[currentIndex].getTag();
    }

    public static void subscribeToTeamListUpdateDel(TeamListUpdateDel del)
    {
        teamListUpdateDel += del;
    }

    // This method will add new unit into the current team
    public static void addNewUnit(GameObject go)
    {
        // adding new unit to the current mainPlayer's list
        teamList[currentIndex].addNewUnit(go);
        Debug.Log("just add one unit, calling team list update");
        teamListUpdateDel();
    }

    // This method will remove the unit from the team with the specific teamTag
    public static void removeUnit(GameObject go, string teamTag)
    {
        for (int i = 0; i < teamList.Count; i++)
        {
            if (teamList[i].getTag() == teamTag)
            {
                teamList[i].removeUnit(go);
            }
        }
        Debug.Log("just remove one unit, calling team list update");
        Debug.Log("team list from Team manager:");
        foreach (GameObject u in teamList[1].getAllUnitsInList())
        {
            Debug.Log(u.name);
        }
        teamListUpdateDel();
    }

    public static void setCurrentTeamIndex(int index)
    {
        currentIndex = index;
    }
}
