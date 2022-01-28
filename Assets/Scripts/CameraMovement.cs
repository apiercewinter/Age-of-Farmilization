using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
// CameraMovement class deals with the movement of the camera
// Players can use WASD keys and scroll wheel to move the camera.
//Player can toggle locked camera mode to follow their player character around using space
=======
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
public class CameraMovement : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float scrollInput;
<<<<<<< HEAD
    
    public bool lockedTrue = false;
    public Transform Target;
=======
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527

    public float movementSpeed = 0.06f;
    public float zoomSpeed = 10.0f;

    public float maxHeight = 40f;
    public float minHeight = 4f;
<<<<<<< HEAD
    
=======

    // Start is called before the first frame update
    void Start()
    {

    }

>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
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
<<<<<<< HEAD

        //Camera is set to lock on player character
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Target = GameObject.FindWithTag("Player").transform;
            LockPlayer();
        }
    }

    public void LockPlayer()
    {
        Vector3 Overhead = new Vector3(10f, 15f, 10f);
        transform.position = Target.position + Overhead;
=======
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
    }
}
