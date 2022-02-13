using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Writer: Boyuan Huang

// SelectionDictionary is a static class and all of its methods are static
// thus all other classes are able to access it 
//
// This class has a dictionry that stores all the selectable objects that are currently selected
// If you want to do anything with the selected objects, just call method getDict()

public class SelectionDictionary : MonoBehaviour
{
    // selectedDictionary is not directly accessible by user
    // If you want to get this dictionary, call the method getDict()
    private static Dictionary<int, GameObject> selectedDictionary = new Dictionary<int, GameObject>();

    // add the GameObject to the selectedDictionary
    public static void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedDictionary.ContainsKey(id)))
        {
            go.gameObject.tag = "Player";
            selectedDictionary.Add(id, go);
        }
    }
    
    // Remove a certain GameObject (identify by its unique instance ID) from the selectionDictionary
    public static void deselect(int id)
    {
        selectedDictionary.Remove(id);
    }

    // Remove all GameObject from the selectionDictionary
    public static void deselectAll()
    {
        disableIndicator();

        deselectCamera();
        selectedDictionary.Clear();
    }

    //Remove all "Player" tags from GameObject in the selectionDictionary
    public static void deselectCamera()
    {
        GameObject[] controlledUnits = GameObject.FindGameObjectsWithTag("Player");
        if (controlledUnits != null)
        {
            foreach(GameObject units in controlledUnits)
            {
                units.gameObject.tag = "Untagged";
            }
        }
    }

    // Return the selectedDictionary
    public static Dictionary<int, GameObject> getDict()
    {
        return selectedDictionary;
    }

    // This method will enable canvas (indicates that the objects are selected) of 
    // all the GameObjects stored in the selectionDictionary
    public static void enableIndicater()
    {
        foreach (GameObject go in selectedDictionary.Values)
        {
            go.GetComponent<UIUnitCentralPublisher>().enableSelectionIndicator();
        }
    }

    // This method will disable canvas (indicates that the objects are deselected) of 
    // all the GameObjects stored in the selectionDictionary
    public static void disableIndicator()
    {
        foreach (GameObject go in selectedDictionary.Values)
        {
            go.GetComponent<UIUnitCentralPublisher>().disableSelectionIndicator();
        }
    }
}
