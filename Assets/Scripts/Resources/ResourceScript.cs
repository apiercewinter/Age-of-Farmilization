//Alec Kaxon-Rupp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceScript : MonoBehaviour
{
    public static event EventHandler OnResourceAmountChanged;
    private static Dictionary<string, int> ResourceTotals = new Dictionary<string, int>();



    //Adds
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


    public static void SubtractResourceAmount(string resource, int amount)
    {
        if (!(ResourceTotals.ContainsKey(resource)))
        {
            ResourceTotals.Add(resource, 0);
        }
        else
        {
            ResourceTotals[resource] = ResourceTotals[resource] - amount;
        }

        if(OnResourceAmountChanged != null)
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

    public static void ClearDict()
    {
        ResourceTotals.Clear();
    }

}
