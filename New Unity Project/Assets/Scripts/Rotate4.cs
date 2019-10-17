using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate4 : MonoBehaviour
{
    float rotationSpeed = 10.0f;

    void OnMouseDrag()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float YaxisRotation = Input.GetAxis("Horizontal") * rotationSpeed;
        // select the axis by which you want to rotate the GameObject
       // transform.RotateAround(Vector3.down, XaxisRotation);
        //transform.RotateAround(Vector3.right, YaxisRotation);
        transform.Rotate(0, YaxisRotation, 0); 
    }
}
