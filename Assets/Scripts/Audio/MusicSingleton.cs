//Aaron Winter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{

    private static MusicSingleton musicSingletonInstance;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);

        //Singleton check
        if(musicSingletonInstance == null)
        {
            musicSingletonInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    
}
