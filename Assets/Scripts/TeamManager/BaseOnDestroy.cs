using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// This is a class that will handle the destroy of the base
public class BaseOnDestroy : MonoBehaviour
{
    /*void Start()
    {
        WinLoseManager.subscribeToDisableControl(disableOnDestroy);
    }*/

    private void OnDestroy()
    {
        if (!TeamManager.getHasWinner())
        {
            Debug.Log(gameObject.tag + " team just called onDestroy");
            TeamManager.removeBase(gameObject.tag);
        }
    }

    /*private void disableOnDestroy()
    {
        Debug.Log(gameObject.tag + " team jsut called diableonDestroy");
        GetComponent<BaseOnDestroy>().enabled = false;
    }*/
}
