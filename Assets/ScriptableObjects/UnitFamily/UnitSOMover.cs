//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitSOMover : UnitSOBase
{
    //Handles all the movement parameter setting
    [SerializeField] protected float moveDistance;

    //To be called after creating the UnitMover component
    public void setupMovement(GameObject go)
    {
        UnitMover us = go.GetComponent<UnitMover>();
        if (!us) return; //Check to make sure it actually has the script

        us.setMoveDistance(moveDistance);

    }
}
