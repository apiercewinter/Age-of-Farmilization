// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorHarvesting : State
{
    private GameObject target;
    private List<GameObject> collectorsList = new List<GameObject>();

    public CollectorHarvesting(List<GameObject> collectorsList, GameObject target)
        : base(null)
    {
        this.collectorsList = collectorsList;
        this.target = target;
    }

    public override void update()
    {
        base.update();
        foreach (GameObject collectorGO in collectorsList)
        {
            if (target == null)
            {
                break;
            }
            UnitCollector myCollector = collectorGO.GetComponent<UnitCollector>();
            float collectRange = myCollector.getRange();

            if (Vector3.Distance(target.transform.position, collectorGO.transform.position) < collectRange)
            {
                myCollector.collect(target);
            }
            else
            {
                UnitMover myMover = collectorGO.GetComponent<UnitMover>();
                float moveDist = myMover.getMoveDistance();
                Vector3 directionToTarget = - (collectorGO.transform.position - target.transform.position).normalized;
                float distBetween = Vector3.Distance(target.transform.position, collectorGO.transform.position);
                if (distBetween > moveDist)
                {
                    myMover.moveRel(directionToTarget * (moveDist - 1));
                }
                else
                {
                    myMover.moveRel(directionToTarget * (distBetween + 1 - collectRange));
                    myCollector.collect(target);
                }
            }
        }
    }
}
