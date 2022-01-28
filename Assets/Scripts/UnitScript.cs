using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
<<<<<<< HEAD
using System;

public class UnitScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public string team;

=======

public class UnitScript : MonoBehaviour
{
    public string team;
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
    public UnitScriptableObject unitData
    {
        get { return myData; }
        set 
        {
            myData = value;
            myAgent = gameObject.GetComponent<NavMeshAgent>();
            myAgent.speed = myData.speed;
<<<<<<< HEAD
            myAgent.stoppingDistance = myData.range;

            //Set model
            Destroy(myModel);
            myModel = Instantiate(myData.modelPrefab);
            myModel.transform.SetParent(gameObject.transform, false);
            myAnimator = myModel.GetComponent<Animator>();
            if (myAnimator) myAnimator.logWarnings = false;
=======

            //Set model
            Destroy(myModel);
            myModel = Instantiate(myData.modelPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
            myAnimator = myModel.GetComponent<Animator>();
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527

        }
    }

    private UnitScriptableObject myData;
    private NavMeshAgent myAgent;
    private GameObject myModel;
    private Animator myAnimator;

<<<<<<< HEAD
    [SerializeField]private GameObject myTarget;
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
=======
    // Start is called before the first frame update
    void Start()
    {

>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
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
                    pScript.target = myTarget;
                    pScript.damage = myData.attack;

                    Debug.Log(gameObject.name + " attacked " + myTarget.name);
                }
            }
        }

        if(resourceToGather)
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

                    Debug.Log(gameObject.name + " gathered " + resourceToGather.name);
                }
=======
        //Animations
        if (myAnimator)
        {
            if (myAgent.remainingDistance == 0)
            {
                myAnimator.SetFloat("Speed_f", 0f);
                myAnimator.SetBool("Static_b", true);
            }
            else
            {
                myAnimator.SetFloat("Speed_f", myData.speed);
                myAnimator.SetBool("Static_b", false);
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
            }
        }
    }

    public void moveTo(Vector3 movementDestination)
    {
<<<<<<< HEAD
        //Stop attacking if doing so
        myTarget = null;
        resourceToGather = null;
        myAgent.stoppingDistance = 1; //Some calculated constant idk
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
        //if (!resource.GetComponent<MonoBehaviour>()) return false; //Temp placeholder before ResourceScript type of class
        if (!resource.GetComponent<ResourceScript>()) return false;

        myTarget = null;
        resourceToGather = resource;
        myAgent.stoppingDistance = gatherDistance;
        myAgent.SetDestination(resourceToGather.transform.position);

        return true;
    }

    private bool animatorHasParameter(string p)
    {
        return Array.IndexOf(myAnimator.parameters, p) >= 0;
    }

=======
        myAgent.SetDestination(movementDestination);
    }

>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
    private void setModelLayerToMine(GameObject m)
    {
        m.layer = gameObject.layer;

        foreach (Transform child in m.transform)
        {
            setModelLayerToMine(child.gameObject);
        }
    }
}
