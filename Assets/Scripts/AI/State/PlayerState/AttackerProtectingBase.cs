// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerProtectingBase : State
{
    List<GameObject> attackerList = new List<GameObject>();
    private Vector3 basePos;

    public AttackerProtectingBase(List<GameObject> attackerList, Vector3 basePos)
        : base(null)
    {
        this.attackerList = attackerList;
        this.basePos = basePos;
    }

    public override void update()
    {
        base.update();
        foreach (GameObject go in attackerList)
        {
            UnitMover myMover = go.GetComponent<UnitMover>();
            float moveDist = myMover.getMoveDistance();
            Vector3 randomPos = Random.insideUnitSphere * (moveDist - 2);
            randomPos.y = 0;
            randomPos += basePos;
            if (Vector3.Distance(go.transform.position, randomPos) > myMover.getMoveDistance())
            {
                Vector3 direction = randomPos.normalized;
                myMover.moveRel(direction * (moveDist - 2));
            }
            else
            {
                go.GetComponent<UnitMover>().move(randomPos);
            }
        }
    }
}
