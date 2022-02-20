//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public readonly string projectileContainer = "Projectiles"; //In heirarchy, for sorting
    public readonly float maxTimeInWorldAdd = 1f;

    private RaycastHit target;
    private float damage;
    private float timeInAir;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Set the container for this projectile in the heirarchy (for sorting)
        if (projectileContainer != "")
        { 
            GameObject container = GameObject.Find(projectileContainer);
            if (!container)
            {
                container = new GameObject(projectileContainer);
            }
            gameObject.transform.SetParent(container.transform);
        }

        Destroy(gameObject, timeInAir + maxTimeInWorldAdd); //Try to hit enemy for X seconds
    }

    protected virtual void FixedUpdate()
    {
        if (target.transform.gameObject)
        {
            //Track target
            Vector3 directionMoved = move(Time.deltaTime);
            gameObject.transform.rotation = Quaternion.LookRotation(directionMoved);
        }
        else
        { //If the target if it is somewhy gone
            Destroy(gameObject);
        }
    }

    //Moves the projectile and returns the velocity vector of that movement
    protected abstract Vector3 move(float time);

    //Called once OnTriggerEnter hits something, not necessarily the target,
    //  although by default it is only called on hitting the target (see OnTriggerEnter)
    protected abstract void hit(GameObject go);

    protected virtual void OnTriggerEnter(Collider other)
    {
        //Check if you have hit the target, and call a 
        if(other.gameObject == target.transform.gameObject)
        {
            hit(other.gameObject);
        }
    }

    public RaycastHit getTarget()
    {
        return target;
    }
    public void setTarget(RaycastHit t)
    {
        target = t;
    }
    public float getDamage()
    {
        return damage;
    }
    public void setDamage(float d)
    {
        damage = d;
    }
    public float getTimeInAir()
    {
        return timeInAir;
    }
    public void setTimeInAir(float t)
    {
        timeInAir = t;
    }

}
