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

    private void Awake()
    {
        //Checks for when a resource amount is changed and calls to update the resources
        ResourceScript.OnResourceAmountChanged += delegate (object sender, EventArgs e)
        {
            UpdateResourceTextObject();
        };
        UpdateResourceTextObject();
        DontDestroyOnLoad(this.gameObject);
    }

    private void UpdateResourceTextObject()
    {
        //Updates all the resource displays
        FoodDisplay.GetComponent<Text>().text = "" + ResourceScript.GetResourceAmount("Food");
        StoneDisplay.GetComponent<Text>().text = "" + ResourceScript.GetResourceAmount("Stone");
        WoodDisplay.GetComponent<Text>().text = "" + ResourceScript.GetResourceAmount("Wood");
        GoldDisplay.GetComponent<Text>().text = "" + ResourceScript.GetResourceAmount("Gold");
        SilverDisplay.GetComponent<Text>().text = "" + ResourceScript.GetResourceAmount("Silver");
    }
}
