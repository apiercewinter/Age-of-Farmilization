// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a class that will handle the destroy of the base
public class BaseOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        if (!TeamManager.getHasWinner())
        {
            if(gameObject != null)
            {
                TeamManager.removeBase(gameObject.tag);
            }
            
        }
    }
}
