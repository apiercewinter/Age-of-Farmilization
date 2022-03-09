// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSeeking : State
{
    private List<GameObject> attackerList = new List<GameObject>();

    public AttackerSeeking(List<GameObject> attackerList)
        : base(null)
    {
        this.attackerList = attackerList;
    }

    public override void update()
    {
        base.update();
        Vector3 randomDirection = new Vector3(Random.value, 0, Random.value).normalized;
        foreach (GameObject go in attackerList)
        {
            UnitMover myMover = go.GetComponent<UnitMover>();
            myMover.moveRel(randomDirection * (myMover.getMoveDistance() - 1));
        }
    }
}
