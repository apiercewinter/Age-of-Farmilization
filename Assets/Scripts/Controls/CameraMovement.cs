// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// CameraMovement class deals with the movement of the camera
// Players can use WASD keys and scroll wheel to move the camera.
// Player can toggle locked camera mode to follow their player character around using space

public class CameraMovement : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float scrollInput;
    
    private bool lockedTrue = false;
    private Vector3 Target;

    [SerializeField] private float movementSpeed = 0.15f;
    [SerializeField] private float zoomSpeed = 10.0f;

    [SerializeField] private float heldSpeed;

    [SerializeField] private GameObject CameraFree;
    [SerializeField] private GameObject CameraLock;

    private float maxHeight = 40f;
    private float minHeight = 4f;
    [SerializeField]
    GameObject teamManager;

    void Start()
    {
        WinLoseManager.subscribeToDisableControl(disableCameraMovement);
        TeamManager.subscribeToLookAtPlayerDel(lookAt);
        TeamManager.subscribeToGodViewDel(godView);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        scrollInput = Input.GetAxis("Mouse ScrollWheel");

        float scrollMovement = Mathf.Log(transform.position.y) * scrollInput * -zoomSpeed;

        if (scrollMovement > 0 && transform.position.y >= maxHeight)
        {
            scrollMovement = 0;
        }
        else if (scrollMovement < 0 && transform.position.y <= minHeight)
        {
            scrollMovement = 0;
        }

        // Camera is set to have a max and min height
        if ((scrollMovement + transform.position.y) > maxHeight)
        {
            scrollMovement = maxHeight - transform.position.y;
        }
        else if ((scrollMovement + transform.position.y) < minHeight)
        {
            scrollMovement = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrollMovement, 0);
        Vector3 lateralMove = movementSpeed * horizontalInput * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= movementSpeed * verticalInput;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        //Camera is set to lock on player character; Daniel Zhang
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lockedTrue = !lockedTrue;
            if (lockedTrue)
            {
                CameraLock.SetActive(true);
                CameraFree.SetActive(false);
            }
            else
            {
                CameraLock.SetActive(false);
                CameraFree.SetActive(true);
            }
        }
        if (lockedTrue && SelectedObject.getSelected() != null)
        {
            LockPositionToCurrentSelected();
        }

        if (EventSystem.current.IsPointerOverGameObject() && checkIsMenu())
        {
            zoomSpeed = 0.0f;
        }
        else
        {
            if (zoomSpeed != 0)
                holdZoomSpeed();
            else
                zoomSpeed = heldSpeed;
        }
    }

    public void LockPositionToCurrentSelected()
    {
        Vector3 Overhead = new Vector3(10f, 15f, 10f);
        Target = SelectedObject.getSelected().transform.position;
        transform.position = Target + Overhead;
    }

    public void LockPosition()
    {
        Vector3 Overhead = new Vector3(10f, 15f, 10f);
        transform.position = Target + Overhead;
    }

    public void holdZoomSpeed()
    {
        heldSpeed = zoomSpeed;
    }

    void disableCameraMovement()
    {
        GetComponent<CameraMovement>().enabled = false;
    }

    void lookAt(GameObject go)
    {
        if(go!=null)
        {
            Target = go.transform.position;
            LockPosition();
        }   
    }

    void godView()
    {
        transform.position = new Vector3(22.84f, 40, 19.9f);
    }

    public void changeMovementSpeed(float ms)
    {
        movementSpeed = ms;
    }
    public void changeZoomSpeed(float zs)
    {
        zoomSpeed = zs;
    }

    //Helps determined if the mouse is currently interacting with a menu; prevents overlap of controls (Daniel Zhang)
    private bool checkIsMenu()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition; //Set PointerEventData to the Mouse Position so that Mouse Position is examined for objects below using rays

        List<RaycastResult> list = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, list);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].gameObject.tag != "Menu")
            {
                list.RemoveAt(i);
                i--;
            }
        }

        return list.Count > 0;
    }
}
