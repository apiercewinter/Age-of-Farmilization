using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDefender : UnitMover
{
    public override bool takeAction(RaycastHit hit)
    {
        GameObject go = hit.transform.gameObject;

        //Takes the action when you click the unit itself
        if(go == gameObject)
        {
            actionAvailable = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
