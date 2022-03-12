using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour
{
    public void OnMouseDown()
    {
        cameraController.instance.followTranform = transform;
    }
}
