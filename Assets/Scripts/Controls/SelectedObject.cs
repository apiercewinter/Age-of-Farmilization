// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObject : MonoBehaviour
{
    private static GameObject currentSelected = null;

    public static void select(GameObject go)
    {
        disableIndicator();
        currentSelected = go;
    }

    public static void deselect()
    {
        disableIndicator();
        currentSelected = null;
    }

    public static void enableIndicator()
    {
        if (currentSelected != null)
        {
            currentSelected.GetComponent<UIUnitCentralPublisher>().enableSelectionIndicator();
        }
    }

    public static void disableIndicator()
    {
        if (currentSelected != null)
        {
            currentSelected.GetComponent<UIUnitCentralPublisher>().disableSelectionIndicator();
        }
    }

    public static GameObject getSelected()
    {
        return currentSelected;
    }
}
