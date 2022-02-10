//Alec Kaxon-Rupp

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject FoodDisplay;
    [SerializeField] private GameObject StoneDisplay;
    [SerializeField] private GameObject WoodDisplay;
    [SerializeField] private GameObject GoldDisplay;
    [SerializeField] private GameObject SilverDisplay;
    public ResourceScript Inventory;

    public void UpdateResourceTextObject()
    {
        //Updates all the resource displays
        FoodDisplay.GetComponent<Text>().text = "" + Inventory.GetResourceAmount("Food");
        StoneDisplay.GetComponent<Text>().text = "" + Inventory.GetResourceAmount("Stone");
        WoodDisplay.GetComponent<Text>().text = "" + Inventory.GetResourceAmount("Wood");
        GoldDisplay.GetComponent<Text>().text = "" + Inventory.GetResourceAmount("Gold");
        SilverDisplay.GetComponent<Text>().text = "" + Inventory.GetResourceAmount("Silver");
    }
}
