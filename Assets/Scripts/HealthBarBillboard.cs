using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBillboard : MonoBehaviour
{

    public Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.transform.forward);
    }
}
