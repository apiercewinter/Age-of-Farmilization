using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// CameraMovement class deals with the movement of the camera
// Players can use WASD keys and scroll wheel to move the camera.
//Player can toggle locked camera mode to follow their player character around using space
public class CameraMovement : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float scrollInput;
    
    public bool lockedTrue = false;
    public Vector3 Target;

    public float movementSpeed = 0.06f;
    public float zoomSpeed = 10.0f;

    public float maxHeight = 40f;
    public float minHeight = 4f;

    private void Start()
    {
        WinLoseManager.subscribeToDisableControl(disableCameraMovement);
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

        //Camera is set to lock on player character
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lockedTrue = !lockedTrue;
        }
        if (lockedTrue)
        {
            LockPosition();
        }
    }

    
    public void FindCenterPosition()
    {
        GameObject[] controlledUnits = GameObject.FindGameObjectsWithTag("Player");

        Vector3 center = new Vector3();
        int total = controlledUnits.Length;

        foreach(GameObject units in controlledUnits)
        {
            center += units.transform.position;
        }

        Target = center / total;
    }

    public void LockPosition()
    {
        FindCenterPosition();
        Vector3 Overhead = new Vector3(10f, 15f, 10f);
        transform.position = Target + Overhead;
    }

    void disableCameraMovement()
    {
        GetComponent<CameraMovement>().enabled = false;
    }
}
