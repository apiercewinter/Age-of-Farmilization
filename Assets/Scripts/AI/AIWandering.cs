using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWandering : AI
{
    // Start is called before the first frame update
    void Start()
    {
        this.tag = "AIAnimal";
        currentState = new Wandering(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        base.performAction();
    }
}
