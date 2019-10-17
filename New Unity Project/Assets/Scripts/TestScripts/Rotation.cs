using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    Quaternion originRotation;
    float angle;
    
    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.rotation;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angle++;
        Quaternion rotationY = Quaternion.AngleAxis(angle, Vector3.up);
        Quaternion rotationX = Quaternion.AngleAxis(angle, Vector3.right);

        //transform.rotation = originRotation * rotationY * rotationX;
        transform.rotation = originRotation * rotationY;
    }
}
