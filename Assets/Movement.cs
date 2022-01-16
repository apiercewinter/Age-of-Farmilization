using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    public Vector3 moveChange;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveChange.Equals(0))
        {
            animator.SetFloat("Speed_f", moveChange.magnitude*10);
            transform.position += moveChange;
        }
    }
}
