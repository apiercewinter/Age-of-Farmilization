using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public float m_startingStock;
    public Transform m_resourceSpawn;

    private float m_currentStock;
    private bool m_washedUp;
    
    public void Awake()
    {
        m_currentStock = m_startingStock;
        m_washedUp = false;

    }

    public void depleteResource(float power)
    {
        //reduce the amount of a resource when used by a player
        m_currentStock -= power;

        if(m_currentStock == 0 && !m_washedUp)
        {
            Depleted();
        }
    }

    public void Depleted()
    {
        m_washedUp = true;

        Destroy(gameObject);
    }
}
