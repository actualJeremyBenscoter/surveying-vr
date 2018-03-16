using Game.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class FootKnob : MonoBehaviour, IInteractable
{
    [Range(0f, 1f)]
    public float Sensitivity = 0.1f;

    public bool IsInteracting { get; private set; }
    private Transform _objectInteracting;
    private Vector3 _startPosition;
    private Vector3 _lastPosition;

    private Vector3 _currentHandPositionOffset;

    private Vector3 _startHandPosition;

    // Update is called once per frame
    void Update()
    {
        if (IsInteracting)
        {
            UpdateHandPositionOffset();
            _lastPosition.y = GetNewTransformHeight();

            transform.localPosition = _lastPosition;
        }
    }

    private void UpdateHandPositionOffset()
    {
        _currentHandPositionOffset = transform.position - _startHandPosition;
    }

    private float GetNewTransformHeight()
    {
        return _startPosition.y + (_currentHandPositionOffset.x * Sensitivity);
    }

    public void BeginInteraction(GameObject go)
    {
        if (!IsInteracting)
        {
            IsInteracting = true;
            _startPosition = transform.localPosition;
            _lastPosition = transform.localPosition;
            _objectInteracting = go.transform;
            _startHandPosition = go.transform.position;
        }
    }

    public void EndInteraction()
    {
        IsInteracting = false;
        _objectInteracting = null;
    }


}
