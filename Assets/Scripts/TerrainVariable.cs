using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainVariable : MonoBehaviour
{
    // Start is called before the first frame update
   
    public GameObject actionController;


    // Start is called before the first frame update
    void Start()
    {

    }



    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (actionController.GetComponent<ActionController>().currentAction == "move" && actionController.GetComponent<ActionController>().waitingTarget == 1)
            {
                Vector3 worldPosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitData;
                if (Physics.Raycast(ray, out hitData, 1000))
                {
                    string name = hitData.collider.gameObject.name;



                    


                    //NEED MODIFICATION LATER ON OBJECT NAMES
                    if (!name.Contains("Building") && !name.Contains("Troop") && !name.Contains("MilitaryVehicle") && !name.Contains("Harvester") && !name.Contains("Cube") && !name.Contains("Resource")) { 

                        worldPosition = hitData.point;
                        
                        actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().move(worldPosition);
                        actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().movingStatus = 1;
                        actionController.GetComponent<ActionController>().waitingTarget = 0;
                        actionController.GetComponent<ActionController>().unitAwaitingTarget = null;
                    }
                }

            }
            else if (actionController.GetComponent<ActionController>().currentAction == "attack" && actionController.GetComponent<ActionController>().waitingTarget == 1)
            {
                Vector3 worldPosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitData;
                if (Physics.Raycast(ray, out hitData, 1000))
                {
                    string name = hitData.collider.gameObject.name;



                    Debug.Log(name);


                    //NEED MODIFICATION LATER ON OBJECT NAMES
                    if ((name.Contains("Building") || name.Contains("Troop") || name.Contains("Harvester") || name.Contains("Cube") || name.Contains("MilitaryVehicle")) && hitData.collider.gameObject!= actionController.GetComponent<ActionController>().unitAwaitingTarget)
                    {

                        worldPosition = hitData.point;


                        //move first to range

                        float temp = Mathf.Pow(Mathf.Abs(hitData.collider.gameObject.transform.position.x - actionController.GetComponent<ActionController>().unitAwaitingTarget.transform.position.x), 2)
                            + Mathf.Pow(Mathf.Abs(hitData.collider.gameObject.transform.position.y - actionController.GetComponent<ActionController>().unitAwaitingTarget.transform.position.y), 2);
                        temp = Mathf.Sqrt(temp);
                        if (temp > actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().range)
                        {
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().move(worldPosition);
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().attackObject = hitData.collider.gameObject;
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().movingStatus = 1;
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().approachingTarget = 1;
                        }



                        else if (actionController.GetComponent<ActionController>().unitAwaitingTarget.name == "Troop")
                        {
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Troop>().attack(hitData.collider.gameObject);
                            
                        }
                        else if (actionController.GetComponent<ActionController>().unitAwaitingTarget.name == "MilitaryVehicle")
                        {
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<MilitaryVehicle>().attack(hitData.collider.gameObject);
                        }
                        else if (actionController.GetComponent<ActionController>().unitAwaitingTarget.name == "Cube")
                        {
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Troop>().attack(hitData.collider.gameObject);
                        }
                        actionController.GetComponent<ActionController>().waitingTarget = 0;
                        actionController.GetComponent<ActionController>().unitAwaitingTarget = null;
                    }
                }

            }
            else if (actionController.GetComponent<ActionController>().currentAction == "collect" && actionController.GetComponent<ActionController>().waitingTarget == 1)
            {
                Vector3 worldPosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitData;
                if (Physics.Raycast(ray, out hitData, 1000))
                {
                    string name = hitData.collider.gameObject.name;



                    


                    //NEED MODIFICATION LATER ON OBJECT NAMES
                    if ((name.Contains("Mine")|| name.Contains("Resource")|| name.Contains("Scrap")) && hitData.collider.gameObject != actionController.GetComponent<ActionController>().unitAwaitingTarget)
                    {

                        worldPosition = hitData.point;


                        //move first to range

                        float temp = Mathf.Pow(Mathf.Abs(hitData.collider.gameObject.transform.position.x - actionController.GetComponent<ActionController>().unitAwaitingTarget.transform.position.x), 2)
                            + Mathf.Pow(Mathf.Abs(hitData.collider.gameObject.transform.position.y - actionController.GetComponent<ActionController>().unitAwaitingTarget.transform.position.y), 2);
                        temp = Mathf.Sqrt(temp);
                        if (temp > actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().range)
                        {
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().move(worldPosition);
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().attackObject = hitData.collider.gameObject;
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().movingStatus = 1;
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Unit>().approachingTarget = 1;
                        }



                        else if (actionController.GetComponent<ActionController>().unitAwaitingTarget.name.Contains("Harvester"))
                        {
                            actionController.GetComponent<ActionController>().unitAwaitingTarget.GetComponent<Harvester>().collect(hitData.collider.gameObject);
                        }
                        
                        
                        actionController.GetComponent<ActionController>().waitingTarget = 0;
                        actionController.GetComponent<ActionController>().unitAwaitingTarget = null;
                    }
                }

            }
        }
    }
}
