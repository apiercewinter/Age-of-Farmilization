//Aaron Winter

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

    public ProjectileScriptableObject projectile;
    public float speed;
    public float range;
    public float attackCooldown;

    public int costFood;
    public int costStone;
    public int costWood;
    public int costSilver;
    public int costGold;

}
