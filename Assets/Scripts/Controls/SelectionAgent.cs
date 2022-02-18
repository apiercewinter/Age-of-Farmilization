using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Writer: Boyuan Huang

// Code heavily adapted from the YouTube tutorials: https://www.youtube.com/watch?v=OL1QgwaDsqo
// This SelectionAgent uses the box collider to detect whether some GameObjects are selected
// When some GameObjects collide with the Mesh Collider created by the drawing a box in the screen,
// OnTriggerEnter() method will be called and add all GameObjects that collide with to the SelectionDictionary

public class SelectionAgent : MonoBehaviour
{
    RaycastHit hit;

    LayerMask selectableLayer;


    // Start is called before the first frame update
    void Start()
    {
        // "Selectable" layer
        selectableLayer = 1 << 7;

        WinLoseManager.subscribeToDisableControl(disableSelectionAgent);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
                {
                    // If user click on anything
                    // or the ray hits anything
                    GameObject objHit = hit.transform.gameObject;
                    SelectedObject.select(objHit);
                }
                else
                {
                    // If user does not click on any selectable object, every thing selected will be deselected
                    SelectedObject.disableIndicator();
                    SelectedObject.deselect();
                }
            }
            else
            {
                // If user does not click on any selectable object, every thing selected will be deselected
                SelectedObject.disableIndicator();
                SelectedObject.deselect();
            }
        }
        SelectedObject.enableIndicator();

    }

    void disableSelectionAgent()
    {
        SelectedObject.disableIndicator();
        SelectedObject.deselect();
        GetComponent<SelectionAgent>().enabled = false;
    }
}