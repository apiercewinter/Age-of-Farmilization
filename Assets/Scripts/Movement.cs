using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Movement : MonoBehaviour
{

    public List<GameObject> selected;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Left click
        {
            //Toggle selection
            RaycastHit hitPos;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, Mathf.Infinity, 1 << 7)) //Layermask 7 (Unit) only
            {
                GameObject unitHit = hitPos.transform.gameObject;

                if (!selected.Remove(unitHit)) //And check that its actually controllable by the player
                {
                    selected.Add(unitHit);
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) //Right click
        {
            RaycastHit hitPos;
            bool hitEnemy = false;

            //Check first if clicked a unit (to attack it)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, Mathf.Infinity, 1 << 7)) //Layermask 7 (Unit) only
            {
                //Implement logic for attacking here
                //  Likely in the form of ~if hitPos.transform.gameObject == ENEMY -> attack
                GameObject unitHit = hitPos.transform.gameObject;
                hitEnemy = !selected.Contains(unitHit); 
                    //!Array.Exists(selected, element => element == unitHit); //for arrays

                if (hitEnemy)
                {
                    //Attack
                    Debug.Log("Attack enemy " + unitHit.name);

                }
            }

            //Check for where player clicked (if didn't click enemy)
            if (!hitEnemy && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, Mathf.Infinity, 1<<6)) //Layermask 6 (Ground) only
            {
                foreach (GameObject unit in selected)
                {
                    NavMeshAgent playerAgent = unit.GetComponent<NavMeshAgent>();
                    Animator animator = unit.GetComponent<Animator>();
                    //We can likely do this within the unit at some point, akin to unit.moveTo ... and it will just run this
                    //  which allows for variable speed, own handling of animations, .....

                    playerAgent.SetDestination(hitPos.point);

                    animator.SetFloat("Speed_f", 0.3f);
                    animator.SetBool("Static_b", false);

                    if (playerAgent.remainingDistance == 0)
                    { //Note that this causes the animation to turn to static standing when
                        //moving after arriving at destination
                        animator.SetFloat("Speed_f", 0f);
                        animator.SetBool("Static_b", true);
                    }
                }
            }
        }

    }


}
