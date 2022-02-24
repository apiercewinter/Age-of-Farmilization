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
        if (ResourceSupply <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DepleteResource()
    {
        ResourceSupply -= gathervalue;
    }

    public int getResourceSupply()
    {
        return ResourceSupply;
    }

    public string getResourcename()
    {
        return resourcetype;
    }

    public int getGatherValue()
    {
        return gathervalue;
    }


}
