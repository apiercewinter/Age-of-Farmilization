using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryScript : MonoBehaviour
{
    public static event EventHandler OnResourceAmountChanged;
    //private static int FoodAmount = 0;
    private static Dictionary<string, int> ResourceTotals = new Dictionary<string, int>();



    public static void AddResourceAmount(string resource, int amount)
    {

        if (!(ResourceTotals.ContainsKey(resource)))
        {
            ResourceTotals.Add(resource, amount);
        }
        else
        {
            ResourceTotals[resource] = ResourceTotals[resource]+amount;
        }

        //Add event for listeners
        if (OnResourceAmountChanged != null)
        {
            OnResourceAmountChanged(null, EventArgs.Empty);
        }
    }


    public static int GetResourceAmount(string resource)
    {
        if (!(ResourceTotals.ContainsKey(resource)))
        {
            ResourceTotals.Add(resource, 0);
        }
        return ResourceTotals[resource];
    }
}
