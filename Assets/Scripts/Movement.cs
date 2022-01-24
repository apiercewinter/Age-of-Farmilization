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
        groundLayer = 1<<LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //right click to move
        {
            RaycastHit hitPos;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, Mathf.Infinity, groundLayer)) //Only when user click on ground layer
            {
                foreach (GameObject go in SelectionDictionary.selectedDictionary.Values)
                {
                    go.GetComponent<UnitScript>().moveTo(hitPos.point);
                }
            }
        }
    }
}
