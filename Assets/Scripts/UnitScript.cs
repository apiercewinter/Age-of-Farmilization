using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{
    public string team;
    public UnitScriptableObject unitData
    {
        get { return myData; }
        set 
        {
            myData = value;
            myAgent = gameObject.GetComponent<NavMeshAgent>();
            myAgent.speed = myData.speed;

            //Set model
            Destroy(myModel);
            myModel = Instantiate(myData.modelPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
            myAnimator = myModel.GetComponent<Animator>();

        }
    }

    private UnitScriptableObject myData;
    private NavMeshAgent myAgent;
    private GameObject myModel;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            }
        }
    }

    public void moveTo(Vector3 movementDestination)
    {
        myAgent.SetDestination(movementDestination);
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
