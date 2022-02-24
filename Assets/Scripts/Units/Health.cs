// Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float m_MaxHealth;

    [SerializeField] private float m_CurrentHealth;

    private UIUnitCentralPublisher UIPublisher;


    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        UIPublisher = GetComponent<UIUnitCentralPublisher>();
        UIPublisher.setMaxHealth(m_MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if (m_CurrentHealth == 0)
        {
            //Death part
            UnitBase unit = GetComponent<UnitBase>();

            if(unit)
            {
                GetComponent<UnitBase>().destroy();
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }

    public void Damage(float d)
    {
        m_CurrentHealth = Math.Max(m_CurrentHealth - d, 0);

        // Notify the UIUnitCentralPublisher's subscribers
        UIPublisher.substractHealth(d);
    }

    public void Heal(float d)
    {
        m_CurrentHealth = Math.Max(m_CurrentHealth + d, m_MaxHealth);

        // Notify the UIUnitCentralPublisher's subscribers
        UIPublisher.addHealth(d);
    }

    public float getHealth()
    {
        return m_CurrentHealth;
    }

    public void setMaxHealth(float h)
    {
        m_MaxHealth = h;
        m_CurrentHealth = h;
    }
}