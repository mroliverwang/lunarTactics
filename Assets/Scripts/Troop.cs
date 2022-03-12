using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{


    public int damage = 10;
    //public int range = 5;
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


        if (actionController.GetComponent<ActionController>().currentAction == "attack" && actionController.GetComponent<ActionController>().waitingTarget != 1)
        {
            
            actionController.GetComponent<ActionController>().waitingTarget = 1;
            actionController.GetComponent<ActionController>().unitAwaitingTarget = gameObject;
        }
    }




    public void attack(GameObject target)
    {
        if (target.name.Contains("Building"))
        {
            target.GetComponent<Building>().health -= gameObject.GetComponent<Troop>().damage;
        }
        else
        {
            target.GetComponent<Unit>().health -= gameObject.GetComponent<Troop>().damage;
        }
        
    }



}
