using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        selectedDictionary.Clear();
    }

    // Return the selectedDictionary
    public static Dictionary<int, GameObject> getDict()
    {
        return selectedDictionary;
    }
}
