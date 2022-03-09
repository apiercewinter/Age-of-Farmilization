//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTargeted : ProjectileBase
{
    protected bool hitTarget = false;

    protected override Vector3 move(float time)
    {
        Vector3 movingTo = getTargetPos();
        float distance = Vector3.Distance(movingTo, transform.position);
        float speed = time*distance/timeRemaining;

        //Track target
        Vector3 directionToMove = (movingTo - gameObject.transform.position).normalized * speed;
        if(directionToMove.magnitude < speed)
        { //On the target but hasnt "hit"
            //Try to move to the gameobject target center now
            directionToMove = (getTarget().transform.position - gameObject.transform.position).normalized * speed;
        }
        gameObject.transform.position += directionToMove;
        return directionToMove;
    }

    protected override void hit(GameObject go)
    {
        if (hitTarget) return;
        hitTarget = true;

        Health enemyHP = go.GetComponent<Health>();
        if (!enemyHP) return;

        enemyHP.Damage(getDamage());

        Destroy(gameObject);
    }

    protected virtual void OnDisable()
    {
        //Failsafe - hit the target one way or another
        if (!getTarget()) return; //Check target exists
        hit(getTarget());
    }
}
