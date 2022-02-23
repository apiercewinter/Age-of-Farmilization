using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResource : MonoBehaviour
{
    TextMeshProUGUI textField;
    GameObject ui;
    private ResourceObject resource;

    // Start is called before the first frame update
    void Start()
    {
        textField = gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        ui = gameObject.transform.GetChild(0).gameObject;
        ui.SetActive(false);
        resource = gameObject.GetComponent<ResourceObject>();
    }

    private void OnMouseOver()
    {
        int resourceSupply = resource.getResourceSupply();
        string name = resource.getResourceName();
        textField.text = "You are looking at " + name + ", it has " + resourceSupply + " left.";
        ui.SetActive(true);
    }

    private void OnMouseExit()
    {
        ui.SetActive(false);
    }
}
