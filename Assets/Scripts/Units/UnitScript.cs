//Aaron Winter
//Alec Kaxon-Rupp (Only worked on code relating to gathering)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class UnitScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; //Different from the ScriptableObject
    public string team
    {
        get { return myTeam; }
        set
        {
            //Only set team if it is blank
            if (myTeam == "") myTeam = value;
        }
    }

    public UnitScriptableObject unitData
    {
        get { return myData; }
        set
        {
            //Set private variables
            myData = value;
            myAgent = gameObject.GetComponent<NavMeshAgent>();
            myAgent.speed = myData.speed;
            myAgent.stoppingDistance = myData.range;

            //Set model
            Destroy(myModel);
            myModel = Instantiate(myData.modelPrefab);
            myModel.transform.SetParent(gameObject.transform, false);
            myAnimator = myModel.GetComponent<Animator>();
            if (myAnimator) myAnimator.logWarnings = false;

        }
    }

    private string myTeam = "";
    private UnitScriptableObject myData;
    private NavMeshAgent myAgent;
    private GameObject myModel;
    private Animator myAnimator;

    [SerializeField] private float movementStoppingDistance;
    [SerializeField] private GameObject myTarget;
    private float nextAttackTime;
    [SerializeField] private float gatherDistance = 4;
    [SerializeField] private float gatherCooldown = 4;
    [SerializeField] private GameObject resourceToGather;
    private float nextGatherTime;

    // Start is called before the first frame update
    void Start()
    {
        nextAttackTime = Time.time;
        nextGatherTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float curSpeed = myAgent.velocity.magnitude;

        //Animations
        if (myAnimator)
        {
            myAnimator.SetFloat("Speed_f", curSpeed);
            myAnimator.SetBool("Static_b", curSpeed == 0);
        }

        if (myTarget)
        {//If you are currently attacking something
            if (Vector3.Distance(myTarget.transform.position, gameObject.transform.position) > myData.range)
            { //Keep following, out of range to attack
                myAgent.SetDestination(myTarget.transform.position);
                myAnimator.SetInteger("Animation_int", 0);
            }
            else
            { //Try to attack now (need to be fully stopped and have cooldown ready)
                myAgent.ResetPath();
                if (nextAttackTime < Time.time && myAgent.velocity.magnitude == 0)
                {   //Attack !!!
                    nextAttackTime = Time.time + myData.attackCooldown;

                    //Spawn projectile and set it to move towards target and only target
                    Vector3 projectileStart = gameObject.GetComponent<BoxCollider>().center + gameObject.transform.position;
                    GameObject projectile = Instantiate(projectilePrefab, projectileStart, Quaternion.identity);
                    ProjectileScript pScript = projectile.GetComponent<ProjectileScript>();
                    pScript.projectileData = myData.projectile;
                    pScript.setTarget(myTarget, myData.attack);

                    //Animation
                    myAnimator.SetInteger("Animation_int", 10); //Attacking Animation (Grenade Throw)

                    Debug.Log(gameObject.name + " attacked " + myTarget.name);
                }
            }
        }
        else
        {
            myAnimator.SetInteger("Animation_int", 0);
        }

        if (resourceToGather)
        {
            if (Vector3.Distance(resourceToGather.transform.position, gameObject.transform.position) > gatherDistance)
            { //Keep moving, can't gather

            }
            else
            { //Try to gather now (need to be fully stopped and have cooldown ready)
                myAgent.ResetPath();
                if (nextGatherTime < Time.time && myAgent.velocity.magnitude == 0)
                {   // Gather !!!
                    nextGatherTime = Time.time + gatherCooldown;
                    ResourceObject rs = resourceToGather.GetComponent<ResourceObject>();
                    ResourceScript.AddResourceAmount(rs.resourcetype, rs.gathervalue);

                }
            }
        }
    }

    public void moveTo(Vector3 movementDestination)
    {
        //Stop attacking if doing so
        myTarget = null;
        resourceToGather = null;
        myAgent.stoppingDistance = movementStoppingDistance; //Some calculated constant
        myAgent.SetDestination(movementDestination);
    }

    public bool target(GameObject target)
    {//Returns whether it was a valid target to attack
        if (!target.GetComponent<UnitScript>()) return false; //Attacking nothing
        if (target.GetComponent<UnitScript>().team == team) return false; //Dont attack teammates :) thanks

        myTarget = target;
        resourceToGather = null;
        myAgent.stoppingDistance = myData.range;
        myAgent.SetDestination(myTarget.transform.position);

        return true;
    }

    public bool gather(GameObject resource)
    {//Returns whether its a valid resource :)

        if (!resource.GetComponent<ResourceObject>()) return false; //Make sure this is a resource

        myTarget = null;
        resourceToGather = resource;
        myAgent.stoppingDistance = gatherDistance;
        myAgent.SetDestination(resourceToGather.transform.position);

        return true;
    }

    private void setModelLayerToMine(GameObject m)
    {
        m.layer = gameObject.layer;

        foreach (Transform child in m.transform)
        {
            setModelLayerToMine(child.gameObject);
        }
    }

    // When an unit has 0 health, call this destroy() method to destroy the gameObject
    // add code here to take care of some businesses before destroying the gameObject
    public void destroy()
    {
        SelectionDictionary.deselect(gameObject.GetInstanceID());
        gameObject.layer = 0;
        // deathEffect will destroy the gameObject when the "animation" is over
        StartCoroutine(deathEffect(2));
    }

    // The gameObject that should die will rotate in z axis to the ground over
    // "time" seconds, and then destroy. This creates a fall to ground effect
    public IEnumerator deathEffect(float time)
    {
        Transform myTransform = gameObject.transform;
        while (true)
        {
            myTransform.rotation = new Quaternion(myTransform.rotation.x, myTransform.rotation.y,
                myTransform.rotation.z + (Time.deltaTime / time), myTransform.rotation.w);
            yield return null;
            if (myTransform.rotation.z >= 0.7f)
            {
                break;
            }
        }
        Destroy(gameObject);
    }
}