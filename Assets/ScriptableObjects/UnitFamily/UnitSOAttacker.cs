//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitSOAttacker : UnitSOMover
{
    //Handles all the attacker parameter setting
    [SerializeField] protected float damage;
    [SerializeField] protected float range;


    //To be called after creating the UnitAttacker component
    public void setupAttacker(GameObject go)
    {
        setupMovement(go);
        UnitAttacker us = go.GetComponent<UnitAttacker>();
        if (!us) return; //Check to make sure it actually has the script

        us.setDamage(damage);
        us.setRange(range);

    }
}