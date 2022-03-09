// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just a simple class that makes the UI face the camera
// rather than rotate with the player
public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
