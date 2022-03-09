// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedObject : MonoBehaviour
{
    private static GameObject currentSelected = null;

    [SerializeField]
    private GameObject ActionUI;

    private static Transform CollectionText;

    private void Start()
    {
        CollectionText = ActionUI.transform.GetChild(0).GetChild(0); ;
    }

    public static void select(GameObject go)
    {
        disableIndicator();
        currentSelected = go;
        if(CollectionText!=null)
        {
            CollectionText.GetComponent<TextMeshProUGUI>().text = "Name: " + currentSelected.name +"\n" + "Health: "+ currentSelected.GetComponent<Health>().getHealth()+ "/" 
            + currentSelected.GetComponent<Health>().getMaxHealth();
        }
        
    }

    public static void deselect()
    {
        disableIndicator();
        currentSelected = null;
        if(CollectionText!=null)
        {
            CollectionText.GetComponent<TextMeshProUGUI>().text = "";
        }
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

    public static void resetAll()
    {
        CollectionText = null;
    }
}
