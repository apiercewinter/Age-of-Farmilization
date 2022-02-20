//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAOE : ProjectileBase
{
    private float blastRadius;
    private float gravity; //Positive

    protected Vector3 curVelocity;

    protected override void Start()
    {
        base.Start();

        //Make it move upwards basically
        curVelocity = getTargetPos()/getTimeInAir();
        float yChange = gravity * getTimeInAir() / 2;
        curVelocity.y += yChange;
    }

    protected override Vector3 move(float time)
    {
        Vector3 acc = new Vector3(0,-gravity*time,0);

        curVelocity += acc;
        gameObject.transform.position += curVelocity*time;
        return curVelocity*time;
    }

    protected override void hit(GameObject go)
    {
        //Hit all health components in a sphere (that aren't on same team)
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        for (uint i = 0; i < colliders.Length; ++i)
        {
            GameObject thingHit = colliders[i].gameObject;
            if (thingHit.tag != gameObject.tag)
            {
                Health enemyHP = thingHit.GetComponent<Health>();
                if (!enemyHP) continue;

                enemyHP.Damage(getDamage());
                //Debug.Log("Hit " + thingHit.name + " for " + getDamage() + " damage. Enemy now has " + enemyHP.m_CurrentHealth + " HP.");
            }
        }

        Destroy(gameObject);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //Dont explode on hitting allied gameobject
        if (other.gameObject.tag != gameObject.tag)
        {
            hit(other.gameObject);
        }
    }

    public float getBlastRadius()
    {
        return blastRadius;
    }
    public void setBlastRadius(float r)
    {
        blastRadius = r;
    }
    public float getGravity()
    {
        return gravity;
    }
    public void setGravity(float g)
    {
        gravity = g;
    }
}
