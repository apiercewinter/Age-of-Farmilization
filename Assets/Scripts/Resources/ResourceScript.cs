//Alec Kaxon-Rupp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceScript : MonoBehaviour
{
    private Dictionary<string, int> ResourceTotals = new Dictionary<string, int>();
    public int cow = 0;
    public ResourcesDisplay Display;

    //Adds
    public void AddResourceAmount(string resource, int amount)
    {

        if (!(ResourceTotals.ContainsKey(resource)))
        {
            ResourceTotals.Add(resource, amount);
        }
        else
        {
            ResourceTotals[resource] = ResourceTotals[resource]+amount;
        }

        Display.UpdateResourceTextObject();
    }


    public void SubtractResourceAmount(string resource, int amount)
    {
        if (!(ResourceTotals.ContainsKey(resource)))
        {
            ResourceTotals.Add(resource, 0);
        }
        else
        {
            ResourceTotals[resource] = ResourceTotals[resource] - amount;
        }

        Display.UpdateResourceTextObject();
    }


    public int GetResourceAmount(string resource)
    {
        if (!(ResourceTotals.ContainsKey(resource)))
        {
            ResourceTotals.Add(resource, 0);
        }
        return ResourceTotals[resource];
    }
}
