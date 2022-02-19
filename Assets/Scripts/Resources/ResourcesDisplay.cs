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

    public void Update()
    {
        //Updates all the resource displays
        FoodDisplay.GetComponent<Text>().text = "" + TeamManager.getResourceAmount("Food");
        StoneDisplay.GetComponent<Text>().text = "" + TeamManager.getResourceAmount("Stone");
        WoodDisplay.GetComponent<Text>().text = "" + TeamManager.getResourceAmount("Wood");
        GoldDisplay.GetComponent<Text>().text = "" + TeamManager.getResourceAmount("Gold");
        SilverDisplay.GetComponent<Text>().text = "" + TeamManager.getResourceAmount("Silver");
    }
}
