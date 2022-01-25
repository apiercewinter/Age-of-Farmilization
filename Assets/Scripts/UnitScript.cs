using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class UnitScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public string team;

    public UnitScriptableObject unitData
    {
        get { return myData; }
        set 
        {
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

    private UnitScriptableObject myData;
    private NavMeshAgent myAgent;
    private GameObject myModel;
    private Animator myAnimator;

    [SerializeField]private GameObject myTarget;
    private float nextAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        nextAttackTime = Time.time;
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
    }

    public void moveTo(Vector3 movementDestination)
    {
        //Stop attacking if doing so
        myTarget = null;
        myAgent.stoppingDistance = 1; //Some calculated constant idk
        myAgent.SetDestination(movementDestination);
    }

    public bool target(GameObject target)
    {//Returns whether it was a valid target to attack
        if (target.GetComponent<UnitScript>().team == team) return false; //Dont attack teammates :) thanks

        myTarget = target;
        myAgent.stoppingDistance = myData.range;
        myAgent.SetDestination(myTarget.transform.position);

        return true;
    }

    private bool animatorHasParameter(string p)
    {
        return Array.IndexOf(myAnimator.parameters, p) >= 0;
    }

    private void setModelLayerToMine(GameObject m)
    {
        m.layer = gameObject.layer;

        foreach (Transform child in m.transform)
        {
            setModelLayerToMine(child.gameObject);
        }
    }
}
