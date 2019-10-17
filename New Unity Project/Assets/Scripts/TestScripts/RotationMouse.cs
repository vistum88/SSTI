using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMouse : MonoBehaviour
{
    Quaternion originRotation;
    float angleHorizontal;
    float angleVertical;
    float mouseSens = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angleHorizontal += Input.GetAxis("Mouse X") * mouseSens;
        angleVertical += Input.GetAxis("Mouse Y ") * mouseSens;

        angleVertical = Mathf.Clamp(angleVertical, -60, 60); 

        Quaternion rotationY = Quaternion.AngleAxis(angleHorizontal, Vector3.up);
        Quaternion rotationX = Quaternion.AngleAxis(-angleVertical, Vector3.right);

        //transform.rotation = originRotation * rotationY * rotationX;
        transform.rotation = originRotation * rotationY * rotationX;
    }
}


