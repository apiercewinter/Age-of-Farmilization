using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Movement : MonoBehaviour
{

    private LayerMask groundLayer;

    void Start()
    {
        groundLayer = 1 << 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //right click to move
        {
            //Toggle selection
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer)) //Only when user click on ground layer
            {
                foreach (GameObject go in SelectionDictionary.getDict().Values)
                {
                    go.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                }
            }
        }
    }
}
