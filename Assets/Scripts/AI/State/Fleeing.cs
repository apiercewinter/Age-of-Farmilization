using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

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
        if (target != null)
        {
            Vector3 direction = (gameObject.transform.position - target.transform.position).normalized;
            gameObject.GetComponent<UnitScript>().moveTo(direction * 10);
        }
    }
}
