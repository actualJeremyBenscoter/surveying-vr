using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class SurveyorHand : MonoBehaviour {
    public OVRInput.Axis1D HandTrigger;
    private GameObject _touchingObject;
    private Joint _joint;

    private void Update()
    {
        if(OVRInput.Get(HandTrigger)>0.05f && _touchingObject != null)
        {
            _joint = _touchingObject.AddComponent<Joint>();
            _joint.anchor = transform.position;
            _joint.connectedBody = GetComponent<Rigidbody>();
        }
        else if (_joint != null)
        {
            //Destroy(_joint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_touchingObject == null)
        {
            _touchingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _touchingObject = null;
    }
}
