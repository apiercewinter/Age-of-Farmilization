using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// SelectionDictionary is a static class and all of its methods are static
// thus all other classes are able to access it 
public class SelectionDictionary : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary<int, GameObject> selectedDictionary = new Dictionary<int, GameObject>();

    public static void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedDictionary.ContainsKey(id)))
        {
            selectedDictionary.Add(id, go);
            Debug.Log("Added " + id + " to selected dict");
        }
    }

    public static void deselect(int id)
    {
        selectedDictionary.Remove(id);
    }

    public static void deselectAll()
    {
        selectedDictionary.Clear();
    }
}
