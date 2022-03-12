using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{


    public int health = 5;
    public float range  = 5;
    private int posX;
    private int posY;
    private float velocity = 5.0f ;
    private Vector3 targetPos;
    public int movingStatus = 0;
    public int approachingTarget = 0;
    public GameObject attackObject;

    public GameObject actionController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            DestroyGameObject();
        }
       
        if(attackObject != null && approachingTarget == 1)
        {
            if(Mathf.Sqrt(Mathf.Pow(Mathf.Abs(attackObject.transform.position.x - transform.position.x), 2) + Mathf.Pow(Mathf.Abs(attackObject.transform.position.z - transform.position.z), 2)) <= range)
            {
                string n = gameObject.name;
                if (n.Contains("Cube"))
                {
                    GetComponent<Troop>().attack(attackObject);
                    approachingTarget = 0;
                    movingStatus = 0;
                    attackObject = null;
                }
                if (n.Contains("Troop"))
                {
                    GetComponent<Troop>().attack(attackObject);
                    approachingTarget = 0;
                    movingStatus = 0;
                    attackObject = null;
                }
                if (n.Contains("MilitaryVehicle"))
                {
                    GetComponent<MilitaryVehicle>().attack(attackObject);
                    approachingTarget = 0;
                    movingStatus = 0;
                    attackObject = null; 
                }
                if (n.Contains("Harvester"))
                {
                    GetComponent<Harvester>().collect(attackObject);
                    approachingTarget = 0;
                    movingStatus = 0;
                    attackObject = null;
                }
            }
            //ATTACK
            
        }


        //removed && movingStatus == 1 in conditions
        if (Mathf.Abs(targetPos.x - transform.position.x) <= 0.2 && Mathf.Abs(targetPos.z - transform.position.z) <= 0.2 && approachingTarget == 0 )
        {
            movingStatus = 0;
            GetComponent<Rigidbody>().mass = 20;
        }

        if (movingStatus == 1 && approachingTarget == 1 && attackObject!=null)
        {
            move(attackObject.transform.position);
            GetComponent<Rigidbody>().mass = 5;
        }
        else if (movingStatus == 1)
        {
            move(targetPos);
            GetComponent<Rigidbody>().mass = 5;
        }

    }



    private void OnMouseDown()

    {
        
        //Debug.Log(actionController.GetComponent<ActionController>().target);
        if (actionController.GetComponent<ActionController>().currentAction == "move" )
        {
            
            actionController.GetComponent<ActionController>().waitingTarget = 1;
            actionController.GetComponent<ActionController>().unitAwaitingTarget = gameObject;
        }

        
    }




    void DestroyGameObject()
    {
        Destroy(gameObject);
    }



    public void move(Vector3 targetPosition)
    {
        
        targetPos = targetPosition;
        Vector3 target = targetPosition;

        //target.y = transform.position.y + transform.localScale.y;
        //target.y = transform.position.y;

        


        Ray ray = Camera.main.ScreenPointToRay(transform.position);

        RaycastHit hitInfo;

        bool hitIndicator = Physics.Raycast(ray, out hitInfo);

        string name = hitInfo.collider.gameObject.name;
        Debug.Log(name);
        if (hitIndicator && name.Contains("Terrain"))
        {
            
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }

        transform.LookAt(target);
        if (gameObject.name.Contains("MilitaryVehicle"))
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }
        if (gameObject.name.Contains("Harvester"))
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }

        /*Vector3 direction = targetPosition - transform.position;

        //NEED MODIFICATION LATER
        direction.y = 0;
        //Vector3 temp = transform.position;
        //temp.y = Terrain.activeTerrain.SampleHeight(transform.position);
        //transform.position = temp;
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * velocity, Space.World);*/

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * velocity);
        Vector3 pos = transform.position;
        pos.y = Terrain.activeTerrain.SampleHeight(transform.position);
        //Debug.Log(pos.y);
        transform.position = pos;
    }

    /*void LateUpdate()
    {
        
    }*/









}
