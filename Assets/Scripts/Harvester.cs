using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{

    public int storage;

    public GameObject actionController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()

    {


        if (actionController.GetComponent<ActionController>().currentAction == "collect" && actionController.GetComponent<ActionController>().waitingTarget != 1)
        {

            actionController.GetComponent<ActionController>().waitingTarget = 1;
            actionController.GetComponent<ActionController>().unitAwaitingTarget = gameObject;
        }
    }

    public void collect(GameObject target)
    {
        target.GetComponent<Resource>().beingCollected = 1;
        
    }


}
