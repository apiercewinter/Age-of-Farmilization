using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : State
{
    private GameObject target;

    public Attacking(GameObject _gameObject, GameObject target)
        : base (_gameObject)
    {
        currentState = STATE.ATTACKING;
        this.target = target;
    }

    public override void update()
    {
        base.update();
        gameObject.GetComponent<UnitScript>().target(target);
    }
}
