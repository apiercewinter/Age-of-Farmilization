using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Writer: Boyuan Huang

// This is a class that will handle the destroy of the base
public class BaseOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        TeamManager.removeBase(gameObject.tag);
    }
}
