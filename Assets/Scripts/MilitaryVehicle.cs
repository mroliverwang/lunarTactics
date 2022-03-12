using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryVehicle : MonoBehaviour
{


    private int damage = 15;
    //public int range = 7;
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
            target.GetComponent<Building>().health -= gameObject.GetComponent<MilitaryVehicle>().damage;
        }
        else
        {
            target.GetComponent<Unit>().health -= gameObject.GetComponent<MilitaryVehicle>().damage;
        }


    }
}
