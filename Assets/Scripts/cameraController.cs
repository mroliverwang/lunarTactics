using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public static cameraController instance;

    public Transform followTranform;
    public Transform cameraTransform;

    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public float minZoom;
    public float maxZoom;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    // Start is called before the first frame update
    void Start()
    {

        instance = this;

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (followTranform != null)
        {
            transform.position = followTranform.position;
        }
        else {
            HandleMouseInput();
            HandleMovementInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTranform = null;
        }
    }

    void HandleMouseInput()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0 && newZoom.z < maxZoom)
            {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
            }
            if (Input.mouseScrollDelta.y < 0 && newZoom.z > minZoom)
            {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitInfo;

            if (plane.Raycast(ray, out hitInfo))
            {
                dragStartPosition = ray.GetPoint(hitInfo);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitInfo;

            if (plane.Raycast(ray, out hitInfo))
            {
                dragCurrentPosition = ray.GetPoint(hitInfo);

                if (newPosition.x <= -250)
                {
                    newPosition.x = -249;
                }
                else if (newPosition.z <= -250)
                {
                    newPosition.z = -249;
                }
                else if (newPosition.x >= 700)
                {
                    newPosition.x = 699;
                }
                else if (newPosition.z >= 700)
                {
                    newPosition.z = 699;
                }
                else
                {
                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }
        }

        if(Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5.0f));
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (newPosition.x <= -250) 
            {
                newPosition.x = -249;
            }
            else if (newPosition.z <= -250)
            {
                newPosition.z = -249;
            }
            else if (newPosition.x >= 700)
            {
                newPosition.x = 699;
            }
            else if (newPosition.z >= 700)
            {
                newPosition.z = 699;
            }
            else
            {
                newPosition += (transform.forward * movementSpeed);
            }
            
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (newPosition.x <= -250)
            {
                newPosition.x = -249;
            }
            else if (newPosition.z <= -250)
            {
                newPosition.z = -249;
            }
            else if (newPosition.x >= 700)
            {
                newPosition.x = 699;
            }
            else if (newPosition.z >= 700)
            {
                newPosition.z = 699;
            }
            else
            {
                newPosition += (transform.forward * -movementSpeed);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (newPosition.x <= -250)
            {
                newPosition.x = -249;
            }
            else if (newPosition.z <= -250)
            {
                newPosition.z = -249;
            }
            else if (newPosition.x >= 700)
            {
                newPosition.x = 699;
            }
            else if (newPosition.z >= 700)
            {
                newPosition.z = 699;
            }
            else
            {
                newPosition += (transform.right * movementSpeed);
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (newPosition.x <= -250)
            {
                newPosition.x = -249;
            }
            else if (newPosition.z <= -250)
            {
                newPosition.z = -249;
            }
            else if (newPosition.x >= 700)
            {
                newPosition.x = 699;
            }
            else if (newPosition.z >= 700)
            {
                newPosition.z = 699;
            }
            else
            {
                newPosition += (transform.right * -movementSpeed);
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        

        if (Input.GetKey(KeyCode.R) && newZoom.z < maxZoom)
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F) && newZoom.z > minZoom)
        {
            newZoom -= zoomAmount;
        }


        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
