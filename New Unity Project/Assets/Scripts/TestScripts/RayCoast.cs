using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCoast : MonoBehaviour
{
    public float rayDistance = 50;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mCurrPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    GameObject obj;
    bool hold = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, ray.direction * rayDistance);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                obj = hit.collider.gameObject;
                if (obj.name == "Plane")
                {
                    hold = true;
                    //mPrevPos = hit.point - hit.collider.gameObject.transform.position;  
                    mPrevPos = -hit.point;
                    mCurrPos = mPrevPos;
                    //hit.collider.gameObject.transform.position.z;
                    //hit.Distnce
                    //hit.point
                    //print("Координаты точки" + hit.point);
                    // print("Координаты коллайдера" + hit.collider.gameObject.transform.position);
                }
            }
        }

        if (Physics.Raycast(ray, out hit) && hold)
        {
                //mCurrPos = hit.point - hit.collider.gameObject.transform.position;
                mCurrPos = -hit.point;

                float angle = Vector3.Angle(mCurrPos, mPrevPos);

                hit.collider.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.Cross(mCurrPos, mPrevPos));
               // hit.collider.gameObject.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.Cross(mCurrPos, mPrevPos));
                //Debug.Log("Угол поворота" + angle + " " + Quaternion.AngleAxis(angle, Vector3.Cross(mCurrPos, mPrevPos)));
        }

        if (Input.GetMouseButtonUp(0))
        {
            hold = false;
        }
    }
}
