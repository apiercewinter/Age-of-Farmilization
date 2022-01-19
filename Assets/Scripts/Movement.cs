using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    public NavMeshAgent playerAgent;
    public Animator animator;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitPos;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, Mathf.Infinity))
            {
                playerAgent.SetDestination(hitPos.point);

                animator.SetFloat("Speed_f", 0.3f);
                animator.SetBool("Static_b", false);
            }
        }

        if (playerAgent.remainingDistance == 0)
        {
            animator.SetFloat("Speed_f", 0f);
            animator.SetBool("Static_b", true);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked character");
    }


}
