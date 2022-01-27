using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System;

// Movement class deals with the movement of the units currently selected by the players
// This class depends on the SelectionDictionary, because this class needs to move all selected
// units at the same time, it needs to know what units are currently selected, which is 
// stored in the SelectionDictionary and accessible by calling getDict() method.

public class PlayerController : MonoBehaviour
{
    private LayerMask groundLayer;

    void Start()
    {
        // "ground" layer
        groundLayer = 1 << 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //right click to move
        {
            //Toggle selection
            RaycastHit hit;

            if(!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) //Only when user click on ground layer
                {
                    GameObject objHit = hit.transform.gameObject;


                    // Iterate through all selected units and use NavMeshAgent's
                    // SetDestination() method to move all the units
                    foreach (GameObject go in SelectionDictionary.getDict().Values)
                    {
                        UnitScript myScript = go.GetComponent<UnitScript>();

                        if (!myScript.target(objHit))
                        {
                            if (!myScript.gather(objHit))
                            {
                                myScript.moveTo(hit.point);
                            }
                        }
                    }
                }
            }
        }
    }
}
