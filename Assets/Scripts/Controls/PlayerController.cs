// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        WinLoseManager.subscribeToDisableControl(disablePlayerController);
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
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) 
                {
                    GameObject objHit = hit.transform.gameObject;
                    GameObject go = SelectedObject.getSelected();
                    

                    if (go != null)
                    {
                        UnitBase myBase = go.GetComponent<UnitBase>();
                        UnitMover myMover = go.GetComponent<UnitMover>();
                        UIUnitCentralPublisher myPublisher = go.GetComponent<UIUnitCentralPublisher>();
                        // If player clicks on the ground
                        if (objHit.tag == "Ground")
                        {
                            if (myMover)
                            {
                                myMover.move(hit.point);
                            }
                            myPublisher.setDestinationPath(hit.point);
                        }
                        // If player clicks on the resource
                        else if (objHit.tag == "Resource")
                        {
                            if (myBase)
                            {
                                myBase.takeAction(hit.transform.gameObject, hit.point);
                            }
                            myPublisher.setGatheringResourcePath(objHit);
                        }
                        // If player clicks on other team's units
                        else if (objHit.tag.StartsWith("Player"))
                        {
                            if (myBase)
                            {
                                myBase.takeAction(hit.transform.gameObject, hit.point);
                            }
                            myPublisher.setAttackingEnemyPath(objHit);
                        }
                        // If players click on some AIAnimal
                        else if (objHit.tag == "AIAnimal")
                        {
                            if (myBase)
                            {
                                myBase.takeAction(hit.transform.gameObject, hit.point);
                            }
                            myPublisher.setAttackingEnemyPath(objHit);
                        }
                    }
                }
            }
        }
    }

    void disablePlayerController()
    {
        GetComponent<PlayerController>().enabled = false;
    }
}
