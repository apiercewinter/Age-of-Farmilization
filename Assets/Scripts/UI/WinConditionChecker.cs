using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionChecker : MonoBehaviour
{
    private Transform firstTeam;
    private Transform secondTeam;

    // Start is called before the first frame update
    // I set this Start() to run after all other class's Start() has been called in the Edit -> Project Settings -> Execution Order Settings,
    // in this way, spawner will spawn the units before these two GetChild() methods are called.
    void Start()
    {
        firstTeam = transform.GetChild(0);
        secondTeam = transform.GetChild(1);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (firstTeam.childCount == 0)
        {
            WinLoseManager.win(secondTeam.name);
        }
        if (secondTeam.childCount == 0)
        {
            WinLoseManager.win(firstTeam.name);
        }
    }
}
