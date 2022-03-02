using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMainPlayer : AI
{
    [SerializeField] private GameObject spawner;
    private List<GameObject> unitList = new List<GameObject>();
    private List<GameObject> collectorList = new List<GameObject>();
    private List<GameObject> attackerList = new List<GameObject>();

    private HashSet<GameObject> targetSet = new HashSet<GameObject>();
    private HashSet<GameObject> resourceSet = new HashSet<GameObject>();
    private HashSet<GameObject> threatSet = new HashSet<GameObject>();

    private GameObject playerBase;
    private GameObject target;
    private GameObject resourceToGather;
    private GameObject threat;

    private State mainPlayerState;

    void Start()
    {
        this.gameObject.tag = "PlayerAI";
    }

    public List<GameObject> getAttackerList()
    {
        return attackerList;
    }

    public List<GameObject> getCollectorList()
    {
        return collectorList;
    }

    private void differentiateUnits()
    {
        foreach (GameObject go in unitList)
        {
            if (go.GetComponent<UnitAttacker>())
            {
                attackerList.Add(go);
            }
            else
            {
                collectorList.Add(go);
            }
        }
    }

    private bool hasTarget()
    {
        return targetSet.Count != 0;
    }

    private bool hasThreat()
    {
        return threatSet.Count != 0;
    }

    private bool has5Attacker()
    {
        return attackerList.Count >= 5;
    }

    private bool isSafe()
    {
        return threatSet.Count == 0;
    }

    private bool hasResourceTarget()
    {
        return resourceSet.Count != 0;
    }

    private void cleanNullinAllSets()
    {
        HashSet<GameObject> newTargetSet = new HashSet<GameObject>();
        HashSet<GameObject> newResourceSet = new HashSet<GameObject>();
        foreach (GameObject go in targetSet)
        {
            if (go != null)
            {
                newTargetSet.Add(go);
            }
        }
        targetSet = newTargetSet;
        foreach (GameObject go in resourceSet)
        {
            if (go != null)
            {
                newResourceSet.Add(go);
            }
        }
        resourceSet = newResourceSet;
        threatSet.Clear();
    }

    private void refreshSet()
    {
        foreach (GameObject attackerGO in attackerList)
        {
            Collider[] colliders = Physics.OverlapSphere(attackerGO.transform.position, attackerGO.GetComponent<UnitAttacker>().getRange());
            foreach (Collider collidedGO in colliders)
            {
                string tag = collidedGO.tag;
                if (tag.StartsWith("Player") && tag != "PlayerAI")
                {
                    targetSet.Add(collidedGO.gameObject);
                }
            }
        }
        foreach (GameObject collectorGO in collectorList)
        {
            Collider[] colliders = Physics.OverlapSphere(collectorGO.transform.position, collectorGO.GetComponent<UnitCollector>().getRange());
            foreach (Collider collidedGO in colliders)
            {
                string tag = collidedGO.tag;
                if (tag.StartsWith("Player") && tag != "PlayerAI")
                {
                    threatSet.Add(collidedGO.gameObject);
                }
                else if (tag == "Resource")
                {
                    resourceSet.Add(collidedGO.gameObject);
                }
            }
        }
    }

    private void decideState()
    {
        // Note that since I make different states for different unit type, I will have to
        // call update() on state immediately after setting a new state

        cleanNullinAllSets();
        playerBase = TeamManager.getBaseByTeamTag("PlayerAI");

        unitList = TeamManager.getUnitsByTeamTag("PlayerAI");
        differentiateUnits();
        refreshSet();

        // ========== Start of Attacker behavior ==========
        // AIPlayer will start finding targets when it has more than 5 attacking unit
        if (!has5Attacker())
        {
            currentState = new AttackerProtectingBase(attackerList, playerBase.transform.position);
        }
        else if (!hasTarget())
        {
            currentState = new AttackerSeeking(attackerList);
        }
        else
        {
            float closetDist = Mathf.Infinity;
            foreach (GameObject go in targetSet)
            {
                float thisDist = Vector3.Distance(go.transform.position, playerBase.transform.position);
                if (thisDist < closetDist)
                {
                    closetDist = thisDist;
                    target = go;
                }
            }
            currentState = new AttackerAttacking(attackerList, target);
        }
        base.performAction();
        // ========== End of Attacker behavior ==========

        // ========== Start of Collector behavior ==========
        // NOTE that I make the main player have the same behavior as the collector
        if (!isSafe())
        {
            float closetDist = Mathf.Infinity;
            foreach (GameObject go in threatSet)
            {
                float thisDist = Vector3.Distance(go.transform.position, playerBase.transform.position);
                if (thisDist < closetDist)
                {
                    closetDist = thisDist;
                    threat = go;
                }
            }
            currentState = new CollectorFleeing(collectorList, threat);
            mainPlayerState = new Fleeing(this.gameObject, threat);
        }
        else if (!hasResourceTarget())
        {
            currentState = new AttackerSeeking(collectorList);
            mainPlayerState = new Seeking(this.gameObject, this.gameObject.GetComponent<UnitMover>().getMoveDistance());
        }
        else
        {
            float closetDist = Mathf.Infinity;
            foreach (GameObject go in resourceSet)
            {
                float thisDist = Vector3.Distance(go.transform.position, playerBase.transform.position);
                if (thisDist < closetDist)
                {
                    closetDist = thisDist;
                    resourceToGather = go;
                }
            }
            currentState = new CollectorHarvesting(collectorList, resourceToGather);
            mainPlayerState = new Harvesting(this.gameObject, resourceToGather);
        }
        mainPlayerState.update();
        base.performAction();
        // ========== End of Collector behavior ==========
    }

    private void trySpawnUnits()
    {
        // PlayerAI will spawn units randomly
        List<uint> unitIndexList = new List<uint>() { 0, 1, 2, 3, 4 };
        while (true)
        {
            int randomIndex = Random.Range(0, unitIndexList.Count);
            GameObject spawnedUnit = spawner.GetComponent<UnitSpawner>().spawnUnit(unitIndexList[randomIndex]);
            if (spawnedUnit == null)
            {
                unitIndexList.RemoveAt(randomIndex);
            }
            if (unitIndexList.Count == 0)
            {
                break;
            }
        }
    }

    public override void performAction()
    {
        trySpawnUnits();
        decideState();
    }
}
