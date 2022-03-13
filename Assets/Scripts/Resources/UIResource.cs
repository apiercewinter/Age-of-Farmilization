//Alec Kaxon-Rupp
//Boyuan Huang
//Daniel Zhang

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This Script is used to allow the user to mouse over a resouce object and see what the resouce is.
//Will also display to the user how much of that resource is remaining.

public class UIResource : MonoBehaviour
{
    private GameObject Highlight;
    private GameObject Display;
    private GameObject Text;

    private ResourceObject resourceObj;

    void Awake()
    {
        string resourceType;
        int resIndex;

        resourceObj = gameObject.GetComponent<ResourceObject>();
        resIndex = this.name.IndexOf('R');
        resourceType = this.name.Substring(0, resIndex);
        Highlight = GameObject.FindGameObjectWithTag(resourceType);

        Display = GameObject.FindGameObjectWithTag("AUI");
        Text = GameObject.FindGameObjectWithTag("ActionText");
    }

    void Start()
    {
        Highlight.SetActive(false);
        Display.SetActive(false);
    }

    void OnMouseOver()
    {
        int resourceSupply = resourceObj.getResourceSupply();
        string name = resourceObj.getResourcename();
        Text.GetComponent<TextMeshProUGUI>().text = "You are looking at " + name + ", it has " + resourceSupply + " left.";
        Highlight.SetActive(true);
        Display.SetActive(true);
    }

    void OnMouseExit()
    {
        Highlight.SetActive(false);
        Display.SetActive(false);
    }
}
