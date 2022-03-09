//Alec Kaxon-Rupp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    
    [SerializeField] private int gathervalue = 0;
    [SerializeField] private string resourcetype;
    [SerializeField] private int ResourceSupply = 1;

    private void Update()
    {

        // Checks to see if the resource object has anymore resources to collect
        // If Resource is empty, the gameobject is deleted
        if (ResourceSupply <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DepleteResource()
    {
        // Depletes Resource
        ResourceSupply -= gathervalue;
    }

    public int getResourceSupply()
    {
        // Returns the amount in a resource
        return ResourceSupply;
    }

    public string getResourcename()
    {
        // Returns the name of the resource
        return resourcetype;
    }

    public int getGatherValue()
    {
        // Returns how much can be gathered at the resource per turn
        return gathervalue;
    }


}
