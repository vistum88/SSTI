using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationQuart : MonoBehaviour
{
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mCurrPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;


    void OnTriggerStay(Collider other)
    {
        mPrevPos = mCurrPos;
        mCurrPos = transform.position - other.transform.position;

        float angle = Vector3.Angle(mPrevPos, mCurrPos);


        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.Cross(mCurrPos, mPrevPos));
    }
}
