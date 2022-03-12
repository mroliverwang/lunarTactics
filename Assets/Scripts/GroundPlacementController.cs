using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    // [SerializedFeild]
    public GameObject placeableObjectPrefab;

    // [SerializedFeild]
    public KeyCode newObjectHotKey = KeyCode.A;

    private GameObject currentPlaceableObject;
    private float mouseWheelRotation;

    // Update is called once per frame
    void Update()
    {
        HandleNewObjectHotKey();

        if (currentPlaceableObject != null) {
            MoveCurrentPlaceableObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotKey() {

        if (Input.GetKeyDown(newObjectHotKey)) {
            if (currentPlaceableObject == null) {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
                currentPlaceableObject.gameObject.name = "Building";
                currentPlaceableObject.GetComponent<BoxCollider>().enabled = false;
            }
            else {
                Destroy(currentPlaceableObject);
            }
        }

    }

    private void MoveCurrentPlaceableObjectToMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        bool hitIndicator = Physics.Raycast(ray, out hitInfo) ;

        string name = hitInfo.collider.gameObject.name;

        if (hitIndicator && name != "Building") {
            Vector3 placement = currentPlaceableObject.transform.position;
            currentPlaceableObject.transform.position = hitInfo.point;
            placement.y += 100;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel() {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked() {
        if (Input.GetMouseButtonDown(0)) {
            currentPlaceableObject.GetComponent<BoxCollider>().enabled = true;
            currentPlaceableObject = null;
        }
    }

}
