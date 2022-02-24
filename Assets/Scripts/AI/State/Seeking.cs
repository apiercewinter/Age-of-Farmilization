// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeking : State
{
    private GameObject target;
    private float range;

    public Seeking(GameObject _gameObject, float _range)
        : base(_gameObject)
    {
        currentState = STATE.SEEKING;
        this.range = _range;
    }

    public override void update()
    {
        base.update();
        Vector3 movement = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)).normalized * 
            gameObject.GetComponent<UnitMover>().getMoveDistance() + gameObject.transform.position;

        gameObject.GetComponent<UnitMover>().move(movement);
    }
}
