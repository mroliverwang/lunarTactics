using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{


    public string currentAction = "move";   // none, move, attack
    public int waitingTarget = 0;
    
    public GameObject unitAwaitingTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
