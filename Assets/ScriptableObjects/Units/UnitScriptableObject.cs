using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObjects/Unit")]
public class UnitScriptableObject : ScriptableObject
{

    public GameObject modelPrefab;

    public string unitName;
    public string unitType;

    public float maxHealth;
    public float attack;

<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> e50c5c28b8cbf917ee55e27f3a12250adbe2452f
    public ProjectileScriptableObject projectile;
    public float speed;
    public float range;
    public float attackCooldown;
<<<<<<< HEAD
=======
    public float speed;
    public float range;
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
=======
>>>>>>> e50c5c28b8cbf917ee55e27f3a12250adbe2452f

}
