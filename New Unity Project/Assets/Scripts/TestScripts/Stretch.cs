using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretch : MonoBehaviour
{
    public void OnVlueChnged(float value)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.position = new Vector3(0, (transform.childCount-i) * value, 0);
        }
    }
}

