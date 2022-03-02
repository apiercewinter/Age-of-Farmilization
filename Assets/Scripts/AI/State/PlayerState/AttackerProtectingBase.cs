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
            float moveDist = go.GetComponent<UnitMover>().getMoveDistance();
            Vector3 randomPos = Random.insideUnitSphere * (moveDist - 2);
            randomPos.y = 0;
            go.GetComponent<UnitMover>().moveRel(randomPos);
        }
    }
}
