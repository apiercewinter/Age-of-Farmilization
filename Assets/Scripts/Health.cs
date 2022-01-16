using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_MaxHealth;

    //Should be private in future
    public float m_CurrentHealth;

    void Start()
    {
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
        m_CurrentHealth -= d;
    }
}
