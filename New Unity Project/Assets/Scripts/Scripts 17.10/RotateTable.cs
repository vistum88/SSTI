using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTable : MonoBehaviour
{
    Vector3 preVector = Vector3.zero;
    Vector3 CurrVector = Vector3.zero;
    float angle;

    void OnTriggerEnter(Collider other)
    {
        CurrVector = transform.parent.position - other.transform.position;
    }

        void OnTriggerStay(Collider other)
    {
    	if(true /*grabPinchAction.GetStateDown()*/)
	    {
		    preVector = CurrVector;
            CurrVector = transform.parent.position - other.transform.position;
    
		    angle = Vector3.Angle(preVector, CurrVector);
		    transform.rotation = Quaternion.AngleAxis(angle, Vector3.Cross(CurrVector, preVector));
	    }	
 
    }

}
