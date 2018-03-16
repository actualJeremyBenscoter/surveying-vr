using Game.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class SurveyorHand : MonoBehaviour {
    public OVRInput.Axis1D HandTrigger;
    private IInteractable _interactable;

    private Vector3 _previousPosition;
    private Vector3 _previousRotation;

    private bool _useGravity;
    private bool _isKinematic;

    private void Update()
    {
        if(OVRInput.Get(HandTrigger)>0.05f && _interactable != null)
        {
            //Debug.Log(_grabbable.gameObject.name + " grabbed");
            _interactable.BeginInteraction(gameObject);
        }
        else if (_interactable != null && _interactable.IsInteracting)
        {
            //Debug.Log(_grabbable.gameObject.name + " released");
            EndInteraction();
            
        }

        _previousPosition = transform.position;
        _previousRotation = transform.localEulerAngles;
    }

    private void EndInteraction()
    {
        _interactable.EndInteraction();

        //Do this later.

        //if(_interactable.gameObject.GetComponent<Rigidbody>())
        //{
        //    Vector3 vel = (transform.position - _previousPosition) / Time.deltaTime;
        //    Vector3 torque = (transform.localEulerAngles - _previousRotation) / Time.deltaTime;

        //    _interactable.gameObject.GetComponent<Rigidbody>().velocity = vel;
        //    _interactable.gameObject.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.VelocityChange);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.name + " trigger enter with " + other.name);
        if (_interactable == null && other.GetComponent<IInteractable>() != null)
        {
            //Debug.Log("grabbable set to " + other.name);
            _interactable = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_interactable != null && other.gameObject == _interactable.gameObject)
        {
            //Debug.Log(gameObject.name + " trigger enter with " + other.name);
            EndInteraction();
            _interactable = null;
        }
    }
}
