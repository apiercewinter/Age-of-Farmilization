using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang
// Alec Kaxon-Rupp - Resources/Inventory

public class Team : MonoBehaviour
{
    private GameObject mainPlayer;
    private GameObject playerBase;
    // unitDict is a dictionary that stores all the units (game objects) that belong to
    // the mainPlayer. The reason why to use dictionary with game object's instance ID
    // as key is that it is easier to remove game objects when they die in this way.
    private Dictionary<int, GameObject> unitDict = new Dictionary<int, GameObject>();
    private Dictionary<string, int> resourceInventory = new Dictionary<string, int>();
    private string tag;

    public Team(GameObject mainPlayer, GameObject playerBase, List<GameObject> units, string tag)
    {
        this.mainPlayer = mainPlayer;
        this.playerBase = playerBase;
        foreach (GameObject go in units)
        {
            unitDict.Add(go.GetInstanceID(), go);
        }
        this.tag = tag;
    }

    // This method will be called when the base of the player is destroyed
    public void destroyAll()
    {
        if (mainPlayer != null)
        {
            Destroy(mainPlayer);
        }
        Debug.Log("not stuck by player");
        if (playerBase != null)
        {
            Destroy(playerBase);
        }
        Debug.Log("not stuck by base");
        tag = "";
        foreach (GameObject go in unitDict.Values)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
        Debug.Log("not stuck by units");
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
        if (!unitDict.ContainsKey(newUnit.GetInstanceID()))
        {
            unitDict.Add(newUnit.GetInstanceID(), newUnit);
        }
    }

    public void removeUnit(GameObject unit)
    {
        if (unitDict.ContainsKey(unit.GetInstanceID()))
        {
            unitDict.Remove(unit.GetInstanceID());
        }
    }

    public GameObject getMainPlayer()
    {
        return mainPlayer;
    }

    public GameObject getBase()
    {
        return playerBase;
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

    public string getTag()
    {
        return tag;
    }

    public bool contain(GameObject go)
    {
        return unitDict.ContainsValue(go);
    }

    public void addToInventory(string resourceType, int amount)
    {
        if (!(resourceInventory.ContainsKey(resourceType)))
        {
            resourceInventory.Add(resourceType, amount);
        }
        else
        {
            resourceInventory[resourceType] = resourceInventory[resourceType] + amount;
        }
    }

    public void subtractFromInventory(string resourceType, int amount)
    {
        if (!(resourceInventory.ContainsKey(resourceType)))
        {
            resourceInventory.Add(resourceType, 0);
        }
        else
        {
            resourceInventory[resourceType] = resourceInventory[resourceType] - amount;
        }
    }

    public int getResourceAmount(string resourceType)
    {
        if (!(resourceInventory.ContainsKey(resourceType)))
        {
            resourceInventory.Add(resourceType, 0);
        }
        return resourceInventory[resourceType];
    }
}
