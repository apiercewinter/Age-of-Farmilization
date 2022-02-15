using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// This is a Singleton class that deals with the team
public class TeamManager : MonoBehaviour
{
    [SerializeField]
    private GameObject unitsHolder;

    private static List<Team> teamList = new List<Team>();

    // Start is called before the first frame update
    void Start()
    {
        instantiateTeam();
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
            Team newTeam = new Team(child.GetChild(0).gameObject, newList);
            teamList.Add(newTeam);
        }
    }

    // This method will determine whether the unit belongs to a mainPlayer
    public bool inSameTeam(GameObject mainPlayer, GameObject unit)
    {
        foreach (Team team in teamList)
        {
            if (team.getMainPlayer() == mainPlayer)
            {
                return team.contain(unit);
            }
        }
        return false;
    }
}
