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
                    UnitScript script = unit.GetComponent<UnitScript>();
                    script.moveTo(hitPos.point);
                }
            }
        }

    }


}
