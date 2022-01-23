using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{
    public string team;
    public UnitScriptableObject myData;

    private NavMeshAgent myAgent;
    private GameObject myModel;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = gameObject.GetComponent<NavMeshAgent>();
        myAgent.speed = myData.speed;

        myAnimator = myModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myAgent.remainingDistance == 0)
        {
            myAnimator.SetFloat("Speed_f", 0f);
            myAnimator.SetBool("Static_b", true);
        }
    }

    public void setModel(GameObject m)
    {
        myModel = m;
        m.transform.SetParent(gameObject.transform);
    }

    public void moveTo(Vector3 movementDestination)
    {
        myAgent.SetDestination(movementDestination);

        myAnimator.SetFloat("Speed_f", myData.speed);
        myAnimator.SetBool("Static_b", false);
    }
}
