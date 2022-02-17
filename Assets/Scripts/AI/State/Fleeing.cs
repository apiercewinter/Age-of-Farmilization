using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleeing : State
{
    private GameObject target;

    public Fleeing(GameObject _gameObject, GameObject target)
        : base(_gameObject)
    {
        currentState = STATE.FLEEING;
        this.target = target;
    }

    public override void update()
    {
        base.update();
        Vector3 direction = (target.transform.position - gameObject.transform.position).normalized;
        gameObject.GetComponent<UnitScript>().moveTo(direction * 10);
    }
}
