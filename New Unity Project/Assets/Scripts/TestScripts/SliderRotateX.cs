using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderRotateX : MonoBehaviour
{
    public void OnVlueChnged(float value)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, value, transform.rotation.eulerAngles.z);
    }
}
