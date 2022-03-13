// Boyuan Huang
//Daniel Zhang (minor bugfix only)
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
        foreach (GameObject go in attackerList)
        {
            UnitMover myMover = go.GetComponent<UnitMover>();
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
            myMover.moveRel(randomDirection * (myMover.getMoveDistance() - 1));
        }
    }
}
