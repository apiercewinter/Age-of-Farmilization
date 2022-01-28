using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    public Vector3 moveChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveChange.Equals(0))
        {
            
            transform.position += moveChange;
        }
    }
}
