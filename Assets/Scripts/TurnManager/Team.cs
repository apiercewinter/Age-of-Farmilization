using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private GameObject mainPlayer;
    private Dictionary<int, GameObject> unitDict = new Dictionary<int, GameObject>();
    private List<GameObject> unitList = new List<GameObject>();

    public Team(GameObject mainPlayer, List<GameObject> units)
    {
        this.mainPlayer = mainPlayer;
        foreach (GameObject go in units)
        {
            unitDict.Add(go.GetInstanceID(), go);
        }
        unitList = units;
    }

    public void setMainPlayerToNull()
    {
        this.mainPlayer = null;
    }

    public void setMainPlayer(GameObject newMainPlayer)
    {
        this.mainPlayer = newMainPlayer;
    }

    public void addNewUnit(GameObject newUnit)
    {
        unitDict.Add(newUnit.GetInstanceID(), newUnit);
    }

    public void removeUnit(GameObject unit)
    {
        unitDict.Remove(unit.GetInstanceID());
    }

    public GameObject getMainPlayer()
    {
        return mainPlayer;
    }

    public List<GameObject> getAllUnitsInList()
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (GameObject go in unitDict.Values)
        {
            newList.Add(go);
        }
        return newList;
    }

    public void switchLayer(string layerName)
    {
        foreach (GameObject go in unitList)
        {
            go.layer = LayerMask.NameToLayer(layerName);
        }
        mainPlayer.layer = LayerMask.NameToLayer(layerName);
    }
}
