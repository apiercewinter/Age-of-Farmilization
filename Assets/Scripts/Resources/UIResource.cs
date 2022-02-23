using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResource : MonoBehaviour
{
    TextMeshProUGUI textField;
    GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        textField = gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        ui = gameObject.transform.GetChild(0).gameObject;
        ui.SetActive(false);
    }

    private void OnMouseOver()
    {
        int resourceSupply = gameObject.GetComponent<ResourceObject>().getResourceSupply();
        textField.text = "You are looking at this, it has " + resourceSupply + " left.";
        ui.SetActive(true);
    }

    private void OnMouseExit()
    {
        ui.SetActive(false);
    }
}
