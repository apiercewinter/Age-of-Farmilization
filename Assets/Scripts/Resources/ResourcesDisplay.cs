using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        InventoryScript.OnResourceAmountChanged += delegate (object sender, EventArgs e)
        {
            UpdateResourceTextObject();
        };
        UpdateResourceTextObject();
    }

    private void UpdateResourceTextObject()
    {
        transform.Find("FoodCounter").Find("FoodAmount").GetComponent<Text>().text = "" + InventoryScript.GetResourceAmount("Food");
        transform.Find("StoneCounter").Find("StoneAmount").GetComponent<Text>().text = "" + InventoryScript.GetResourceAmount("Stone");
        transform.Find("WoodCounter").Find("WoodAmount").GetComponent<Text>().text = "" + InventoryScript.GetResourceAmount("Wood");
    }
}
