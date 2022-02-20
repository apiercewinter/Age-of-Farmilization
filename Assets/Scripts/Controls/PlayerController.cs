using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System;

// Writer: Boyuan Huang
// Editor:

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
                    Debug.Log("hitting ground?: objHit.pos as follows:");
                    Debug.Log("hit.point is: " + hit.point);
                    Debug.Log("objHit.point is: " + objHit.transform.position);
                    GameObject go = SelectedObject.getSelected();

                    if (go != null)
                    {
                        UnitScript myScript = go.GetComponent<UnitScript>();
                        UIUnitCentralPublisher myPublisher = go.GetComponent<UIUnitCentralPublisher>();
                        // If player clicks on the ground
                        if (objHit.tag == "Ground")
                        {
                            myScript.moveTo(hit.point);
                            myPublisher.setDestinationPath(hit.point);
                        }
                        // If player clicks on the resource
                        else if (objHit.tag == "Resource")
                        {
                            myScript.gather(objHit);
                            myPublisher.setGatheringResourcePath(objHit);
                        }
                        // If player clicks on other team's units
                        else if (objHit.tag.StartsWith("Player"))
                        {
                            myScript.target(objHit);
                            myPublisher.setAttackingEnemyPath(objHit);
                        }
                        // If players click on some AIAnimal
                        else if (objHit.tag == "AIAnimal")
                        {
                            // This is the most troublesome case, because some AIAnimal has two collider
                            // one works for detecting enemy in range, another works for raycasting.
                            // The detecting enemy collider is much larger collider than the model itself, so even if 
                            // player just clicks on the area near some AIAnimal, the system will think player
                            // is trying to click on the AIAnimal, becasue player click on the much larger
                            // detecting enemy collider, so we need to know whether player is trully clicking
                            // on an AIAnimal by calculating the distance between the point player clicks on
                            // and the objHit's position
                            Vector3 objHitV = objHit.transform.position;
                            Vector3 hitPointV = hit.point;
                            // We only care about the horizontal distance between two points
                            objHitV.y = 0;
                            hitPointV.y = 0;
                            if (Vector3.Distance(objHitV, hitPointV) < 2)
                            {
                                myScript.target(objHit);
                                myPublisher.setAttackingEnemyPath(objHit);
                            }
                            else
                            {
                                myScript.moveTo(hit.point);
                                myPublisher.setDestinationPath(hit.point);
                            }
                        }

                        /*if (!myScript.target(objHit))
                        {
                            if (!myScript.gather(objHit))
                            {
                                myScript.moveTo(hit.point);
                                myPublisher.setDestinationPath(hit.point);
                            }
                            else
                            {
                                myPublisher.setGatheringResourcePath(objHit);
                            }
                        }
                        else
                        {
                            myPublisher.setAttackingEnemyPath(objHit);
                        }*/
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
