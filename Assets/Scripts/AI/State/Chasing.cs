using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

public class Chasing : State
{
    private GameObject target;

    public Chasing(GameObject _gameObject, GameObject target)
        : base(_gameObject)
    {
        currentState = STATE.SEEKING;
        this.target = target;
    }

    public override void update()
    {
        base.update();
        gameObject.GetComponent<UnitScript>().moveTo(target.transform.position);
    }
}
