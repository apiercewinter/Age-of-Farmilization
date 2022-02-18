//Alec Kaxon-Rupp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    
    public int gathervalue = 0;
    public string resourcetype;
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




}
