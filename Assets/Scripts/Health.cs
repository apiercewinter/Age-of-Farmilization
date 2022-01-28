using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using System;

public delegate void myDel(float d);
=======
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_MaxHealth;

    //Should be private in future
    public float m_CurrentHealth;

<<<<<<< HEAD
    public myDel updateHealthDel;

=======
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
    void Start()
    {
        m_MaxHealth = gameObject.GetComponent<UnitScript>().unitData.maxHealth;
        m_CurrentHealth = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(m_CurrentHealth == 0)
        {
            //Death part
            Destroy(gameObject);
        }
    }

    public void Damage(float d)
    {
<<<<<<< HEAD
        if (gameObject.GetComponent<UnitScript>().unitData.maxHealth != m_MaxHealth)
        {//Check if the Scriptable Object has been changed for whatever reason
            m_MaxHealth = gameObject.GetComponent<UnitScript>().unitData.maxHealth;
            m_CurrentHealth = m_MaxHealth;
        }
        m_CurrentHealth = Math.Max(m_CurrentHealth-d, 0);

        updateHealthDel(d);
=======
        m_CurrentHealth -= d;
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
    }
}
