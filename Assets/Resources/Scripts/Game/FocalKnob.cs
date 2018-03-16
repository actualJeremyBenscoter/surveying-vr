using Game.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class FocalKnob : MonoBehaviour, IInteractable
{
    public PostProcessingProfile PostProcess;

    [Range(0f,1f)]
    public float Sensitivity = 0.5f;

    public bool IsInteracting { get; private set; }
    private Transform _objectInteracting;
    private float _startRotationZ;
    private DepthOfFieldModel.Settings _startDOFSettings;
    private DepthOfFieldModel.Settings _lastDOFSettings;
    private float _currentRotationZOffset;

    // Update is called once per frame
    void Update()
    {
        if(IsInteracting)
        {
            UpdateRotationZOffset();
            _lastDOFSettings.focusDistance = GetNewFocalDistance();
            PostProcess.depthOfField.settings = _lastDOFSettings;
        }
    }

    private void UpdateRotationZOffset()
    {
        _currentRotationZOffset = _objectInteracting.transform.localEulerAngles.z - _startRotationZ;
    }

    private float GetNewFocalDistance()
    {
        return Mathf.Clamp(_startDOFSettings.focusDistance + (_currentRotationZOffset * Sensitivity), 0f, 100f);
    }

    public void BeginInteraction(GameObject go)
    {
        if(!IsInteracting)
        {
            IsInteracting = true;
            _startDOFSettings = PostProcess.depthOfField.settings;
            _lastDOFSettings = PostProcess.depthOfField.settings;
            _objectInteracting = go.transform;
            _startRotationZ = go.transform.localEulerAngles.z;
        }
    }

    public void EndInteraction()
    {
        IsInteracting = false;
        _objectInteracting = null;
    }


}
