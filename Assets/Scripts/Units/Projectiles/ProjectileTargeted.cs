//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTargeted : ProjectileBase
{
    protected override Vector3 move(float time)
    {
        Vector3 movingTo = getTarget().point;
        float speed = time/getTimeInAir();

        //Track target
        Vector3 directionToMove = (movingTo - gameObject.transform.position).normalized * speed;
        gameObject.transform.position += directionToMove;
        return directionToMove;
    }

    protected override void hit(GameObject go)
    {
        Health enemyHP = go.GetComponent<Health>();
        if (!enemyHP) return;

        enemyHP.Damage(getDamage());
        Debug.Log("Hit " + go.name + " for " + getDamage() + " damage. Enemy now has " + enemyHP.getHealth() + " HP.");

        Destroy(gameObject);
    }
}
