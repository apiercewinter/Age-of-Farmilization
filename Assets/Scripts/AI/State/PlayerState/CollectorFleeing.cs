// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorFleeing : State
{
    private List<GameObject> collectorsList = new List<GameObject>();
    private GameObject threat;

    public CollectorFleeing(List<GameObject> collectorsList, GameObject threat)
        :base(null)
    {
        this.collectorsList = collectorsList;
        this.threat = threat;
    }

    public override void update()
    {
        base.update();
        Vector3 threatPos = threat.transform.position;
        foreach (GameObject go in collectorsList)
        {
            Vector3 direction = - (threatPos - go.transform.position).normalized;
            UnitMover myMover = go.GetComponent<UnitMover>();
            myMover.moveRel(direction * (myMover.getMoveDistance() - 1));
        }
    }
}
