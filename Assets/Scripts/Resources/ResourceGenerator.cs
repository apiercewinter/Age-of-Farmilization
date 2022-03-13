//Daniel Zhang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private GameObject[] resourceLocations;

    [SerializeField]
    private GameObject[] resources;
    [SerializeField]
    private GameObject resourcesParent;

    //Procedurally generates resources across the map
    void Awake()
    {
        if(resourceLocations == null)
            resourceLocations = GameObject.FindGameObjectsWithTag("ResourceL");

        foreach(GameObject resourceLocations in resourceLocations)
        {
            Instantiate(resources[Random.Range(0, 3)], resourceLocations.transform.position, resourceLocations.transform.rotation, resourcesParent.transform);
        }

    }
}
