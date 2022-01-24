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
        groundLayer = LayerMask.NameToLayer("ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //right click to move
        {
            //Toggle selection
            RaycastHit hitPos;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, Mathf.Infinity, groundLayer)) //Only when user click on ground layer
            {
                foreach (GameObject go in SelectionDictionary.selectedDictionary.Values)
                {
                    go.GetComponent<NavMeshAgent>().SetDestination(hitPos.point);
                }
            }
        }
    }
}
