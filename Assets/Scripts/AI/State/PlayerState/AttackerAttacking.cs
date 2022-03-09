// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAttacking : State
{
    private List<GameObject> attackerList = new List<GameObject>();
    private GameObject target;

    public AttackerAttacking(List<GameObject> attackerList, GameObject target)
        : base(null)
    {
        this.attackerList = attackerList;
        this.target = target;
    }

    public override void update()
    {
        base.update();
        foreach (GameObject attackerGO in attackerList)
        {
            if (target == null)
            {
                break;
            }
            UnitAttacker myAttacker = attackerGO.GetComponent<UnitAttacker>();
            float attackRange = myAttacker.getRange();
            float distBetween = Vector3.Distance(target.transform.position, attackerGO.transform.position);
            if (distBetween < attackRange)
            {
                myAttacker.attack(target);
            }
            else
            {
                UnitMover myMover = attackerGO.GetComponent<UnitMover>();
                float moveDist = myMover.getMoveDistance();
                Vector3 directionToTarget = - (attackerGO.transform.position - target.transform.position).normalized;
                if (distBetween > moveDist)
                {
                    myMover.moveRel(directionToTarget * (moveDist - 1));
                }
                else
                {
                    myMover.moveRel(directionToTarget * (distBetween + 1 - attackRange));
                    myAttacker.attack(target);
                }
            }
        }
    }
}
