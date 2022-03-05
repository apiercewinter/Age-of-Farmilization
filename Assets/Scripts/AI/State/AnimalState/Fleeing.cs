// Boyuan Huang
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
        if (target != null)
        {
            Vector3 movement = (gameObject.transform.position - target.transform.position).normalized *
                gameObject.GetComponent<UnitMover>().getMoveDistance() + gameObject.transform.position;
            gameObject.GetComponent<UnitMover>().move(movement);
        }
    }
}
