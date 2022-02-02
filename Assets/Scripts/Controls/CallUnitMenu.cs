using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CallUnitMenu : MonoBehaviour
{
    //Writer: Daniel Zhang

    //CallUnitMenu class deals with calling the menu to build units
    //Right-click when units aren't currently selected in order to call the Unit Menu and Left-Click to unsummon it

    [SerializeField] GameObject pl_menu; //references the object wanted to be called by the player; may be changed as game scale is expanded

    // Update is called once per frame
    void Update()
    {    
        if (Input.GetMouseButtonDown(1) && checkSelect())
        {
            moveMenu();
        }

        if(Input.GetMouseButtonDown(0) && !checkUsingMenu())
        {
            pl_menu.SetActive(false);
        }

    }
    
    private void moveMenu()
    {
        pl_menu.SetActive(false);

        pl_menu.transform.position = Input.mousePosition;
        pl_menu.SetActive(true);
    }

    bool checkSelect()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
            return true;
        else
            return false;
    }

    bool checkIsMenu()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition; //Set PointerEventData to the Mouse Position so that Mouse Position is examined for objects below using rays

        List<RaycastResult> list = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, list);
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].gameObject.tag != "Menu")
            {
                list.RemoveAt(i);
                i--;
            }
        }

        return list.Count > 0;
    }
    
    bool checkUsingMenu()
    {
        if (EventSystem.current.IsPointerOverGameObject() && checkIsMenu())
            return true;
        else
            return false;
    }
}
