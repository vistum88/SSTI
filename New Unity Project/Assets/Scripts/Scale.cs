using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        transform.localScale = new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2f, transform.localScale.z / 2f);    
    }
}
