using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoordinates : MonoBehaviour
{
    public float rayDistance = 50;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mCurrPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    Vector2 mousePos = new Vector2();
    GameObject obj;
    bool hold = false;

    void Start()
    {

    }

    void Update()
    {

        mousePos.x = -Input.mousePosition.x*5;
        mousePos.y = -Input.mousePosition.y*5;

       // mousePos.x = Input.GetAxis("Horizontal");
       // mousePos.y = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            mPrevPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            mPrevPos = transform.InverseTransformPoint(mPrevPos);
            hold = true;

                mCurrPos = mPrevPos;
               print("Координаты точки" + mPrevPos);
        }

        if (hold)
        {
            mCurrPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            mCurrPos = transform.InverseTransformPoint(mCurrPos);
            float angle = Vector3.Angle(mCurrPos, mPrevPos);

            transform.rotation = transform.rotation * Quaternion.AngleAxis(angle, Vector3.Cross(mCurrPos, mPrevPos));
            //transform.localRotation = Quaternion.AngleAxis(angle, Vector3.Cross(mCurrPos, mPrevPos));
            Debug.Log("Угол поворота" + angle+ " " + mousePos.x+ " " +mousePos.y+ " "+ mPrevPos  + mCurrPos + Quaternion.AngleAxis(angle, Vector3.Cross(mCurrPos, mPrevPos)));
            mPrevPos = mCurrPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            hold = false;
        }
    }
}
