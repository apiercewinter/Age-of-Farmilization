//Daniel Zhang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject P1Base;
    [SerializeField] 
    private GameObject P2Base;

    [SerializeField]
    private GameObject P1Spawner;
    [SerializeField]
    private GameObject P2Spawner;

    [SerializeField]
    private GameObject base1contain;
    [SerializeField]
    private GameObject base2contain;

    [SerializeField]
    private GameObject spawnersParent;

    [SerializeField]
    private Transform[] baseLocations;
    private Transform base1Location;
    private Transform base2Location;

    //Chooses 2 Random locations on the map to spawn the bases of the players or AI
    void Awake()
    {
        int index = Random.Range(0, 4);
        int indexHolder = index; //stores the current index for usage in making sure 2 different locations are used; would be a list if the game were to implement more than 2 players
        SetBase(P1Base, base1Location, index, P1Spawner, base1contain);
        while(index == indexHolder)
        {
            index = Random.Range(0, 4);
        }
        SetBase(P2Base, base2Location, index, P2Spawner, base2contain);
    }

    private void SetBase(GameObject pbase, Transform location, int index, GameObject spawner, GameObject container)
    {
        location = baseLocations[index];
        Instantiate(pbase, location.position, location.rotation, container.transform);
        Vector3 spawnerPos = location.transform.position + location.transform.forward * 10;
        spawner.transform.position = spawnerPos;
        spawner.transform.rotation = location.rotation;
    }

}
