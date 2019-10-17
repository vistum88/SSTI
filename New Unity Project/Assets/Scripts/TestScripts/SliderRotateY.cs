using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderRotateY : MonoBehaviour
{
    public void OnVlueChnged(float value)
    {
        transform.rotation = Quaternion.Euler(value, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
