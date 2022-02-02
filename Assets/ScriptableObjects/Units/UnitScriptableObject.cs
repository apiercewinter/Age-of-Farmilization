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

    [SerializeField] private int costFood;
    [SerializeField] private int costStone;
    [SerializeField] private int costWood;
    [SerializeField] private int costX;
    [SerializeField] private int costY;

    public int GetFoodCost()
    {
        return costFood;
    }

    public int GetStoneCost()
    {
        return costStone;
    }

    public int GetWoodCost()
    {
        return costWood;
    }

}
