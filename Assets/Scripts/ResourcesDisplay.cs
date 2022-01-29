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
        InventoryScript.OnFoodAmountChanged += delegate (object sender, EventArgs e)
        {
            UpdateResourceTextObject();
        };
        UpdateResourceTextObject();
    }

    private void UpdateResourceTextObject()
    {
        transform.Find("FoodAmount").GetComponent<Text>().text = "" + InventoryScript.GetFoodAmount();
    }
}
