// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AIMainPlayer is the class that the AI opponent's main player should have.
//
// AIMainPlayer it self acts the same its collector:
// they will start Seeking for resource in the world
// as soon as any collector unit finds a resource, all collector unit will go to that resource and
// collect it. If collector units meets any enemy unit, all collector units will start Fleeing from
// that unit.
// 
// Attacker unit:
// When there are at least 5 attacker units, all attacker units will start Seeking enemies.
// If there are less than 5 attacker units, all attacker units will just ProtectingBase.
//
// Unit spawning logic:
// AIMainPlayer will try to spawn a random unit every turn, and only one unit per turn, if it
// has enough resource to spawn any unit, it will spawn it

public class AIMainPlayer : AI
{
    private GameObject spawner;
    private List<GameObject> unitList = new List<GameObject>();
    private List<GameObject> collectorList = new List<GameObject>();
    private List<GameObject> attackerList = new List<GameObject>();

    private HashSet<GameObject> targetSet = new HashSet<GameObject>();
    private HashSet<GameObject> resourceSet = new HashSet<GameObject>();
    private HashSet<GameObject> threatSet = new HashSet<GameObject>();
    private HashSet<GameObject> threatToBaseSet = new HashSet<GameObject>();

    private GameObject playerBase;
    private GameObject target;
    private GameObject resourceToGather;
    private GameObject threat;

    void Awake()
    {
        this.gameObject.tag = "PlayerAI";
        // I don't want to use Find() here, but I can't think of any other 
        // better ways to get a reference to the spawner
        spawner = GameObject.Find("PlayerAISpawner");
        collectorList.Add(this.gameObject);
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
        attackerList.Clear();
        collectorList.Clear();
        foreach (GameObject go in unitList)
        {
            if (go.GetComponent<UnitAttacker>())
            {
                attackerList.Add(go);
            }
            else if (go.GetComponent<UnitCollector>())
            {
                collectorList.Add(go);
            }
        }
        collectorList.Add(this.gameObject);
    }

    private bool hasTarget()
    {
        return targetSet.Count != 0;
    }

    private bool hasThreat()
    {
        return threatSet.Count != 0;
    }

    private bool hasThreatToBase()
    {
        return threatToBaseSet.Count != 0;
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
            Collider[] colliders = Physics.OverlapSphere(attackerGO.transform.position, attackerGO.GetComponent<UnitMover>().getMoveDistance() * 2.7f);
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
            Collider[] colliders = Physics.OverlapSphere(collectorGO.transform.position, collectorGO.GetComponent<UnitMover>().getMoveDistance());
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

        threatToBaseSet.Clear();
        Collider[] collidersToBase = Physics.OverlapSphere(playerBase.transform.position, 10);
        {
            foreach (Collider collidedGO in collidersToBase)
            {
                string tag = collidedGO.tag;
                if (tag.StartsWith("Player") && tag != "PlayerAI")
                {
                    threatToBaseSet.Add(collidedGO.gameObject);
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
        if (!has5Attacker() && !hasThreatToBase())
        {
            currentState = new AttackerProtectingBase(attackerList, playerBase.transform.position);
        }
        else if (!has5Attacker() && hasThreatToBase())
        {
            float closetDist = Mathf.Infinity;
            foreach (GameObject go in threatToBaseSet)
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
        else if (!hasTarget() && has5Attacker())
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
        }
        else if (!hasResourceTarget())
        {
            currentState = new AttackerSeeking(collectorList);
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
        }
        base.performAction();
        // ========== End of Collector behavior ==========
    }

    private void trySpawnUnits()
    {
        // PlayerAI will spawn units randomly
        // AI does not spawn healer, this is intentional
        List<uint> unitIndexList = new List<uint>() { 0, 1, 4 };
        if (collectorList.Count < 4)
        {
            unitIndexList.Add(2);
        }

        while (true)
        {
            int randomIndex = Random.Range(0, unitIndexList.Count);
            GameObject spawnedUnit = spawner.GetComponent<UnitSpawner>().spawnUnit(unitIndexList[randomIndex]);

            if (spawnedUnit == null)
            {
                unitIndexList.RemoveAt(randomIndex);
            }
            else
            {
                break;
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