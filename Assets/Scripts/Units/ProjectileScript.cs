using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public string projectileContainer = ""; //In heirarchy, for sorting

    public GameObject target;
    public float damage;

    public ProjectileScriptableObject projectileData
    {
        get { return myData; }
        set
        {
            myData = value;

            //Set model
            Destroy(myModel);
            myModel = Instantiate(myData.modelPrefab);
            myModel.transform.SetParent(gameObject.transform, false);
            myAnimator = myModel.GetComponent<Animator>();
            if(myAnimator) myAnimator.logWarnings = false;

        }
    }

    private ProjectileScriptableObject myData;
    private GameObject myModel;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (projectileContainer != "")
        {
            GameObject container = GameObject.Find(projectileContainer);
            if (!container)
            {
                container = new GameObject(projectileContainer);
            }
            gameObject.transform.SetParent(container.transform);
        }

        Destroy(gameObject, 2); //Try to hit enemy for 2 seconds
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            BoxCollider movingTo = target.GetComponent<BoxCollider>();
            if (!movingTo) return;

            Vector3 directionToMove = ((target.transform.position + movingTo.center) - gameObject.transform.position).normalized * myData.speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.LookRotation(directionToMove);
            gameObject.transform.position += directionToMove;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        { //Do damage
            Health enemyHP = target.GetComponent<Health>();
            if (!enemyHP) return;

            enemyHP.Damage(damage);
            Debug.Log("Hit " + target.name + " for " + damage + " damage. Enemy now has " + enemyHP.m_CurrentHealth + " HP.");

            Destroy(gameObject);
        }
    }

}
