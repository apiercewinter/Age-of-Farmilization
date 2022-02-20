using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

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
        gameObject.GetComponent<UnitMover>().move(new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0));
    }
}
