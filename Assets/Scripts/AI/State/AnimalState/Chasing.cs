// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : State
{
    private GameObject target;
    private float moveDistance;

    public Chasing(GameObject _gameObject, GameObject target)
        : base(_gameObject)
    {
        this.target = target;
        currentState = STATE.CHASING;
        moveDistance = gameObject.GetComponent<UnitMover>().getMoveDistance();
    }

    public override void update()
    {
        base.update();
        if (target != null)
        {
            Vector3 direction = (target.transform.position - gameObject.transform.position).normalized;
            UnitMover myMover = gameObject.GetComponent<UnitMover>();
            if (Vector3.Distance(target.transform.position, gameObject.transform.position) >= moveDistance)
            {
                myMover.move(direction * (moveDistance - 1) + gameObject.transform.position);
            }
            else
            {
                myMover.move(direction * (moveDistance - 1));
            }
        }
    }
}
