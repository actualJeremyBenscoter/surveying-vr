using Game.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehaviour : MonoBehaviour, IInteractable {

    private GameObject _grabber;

    private bool _grabbed;
    private bool _useGravity;
    private bool _isKinematic;
    private Rigidbody _rb;
    private Transform _parent;
    public bool IsInteracting { get { return _grabbed; } }

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
    public void BeginInteraction(GameObject grabber)
    {
        if (!_grabbed)
        {
            _parent = transform.parent;
            transform.parent = grabber.transform;

            if (_rb)
            {
                _useGravity = _rb.useGravity;
                _isKinematic = _rb.isKinematic;

                _rb.useGravity = false;
                _rb.isKinematic = true;
            }
            _grabbed = true;
        }
    }

    public void EndInteraction()
    {
        if (_grabbed)
        {
            _grabbed = false;
            transform.parent = _parent;

            if (_rb)
            {
                _rb.useGravity = _useGravity;
                _rb.isKinematic = _isKinematic;
            }
        }
    }
}
