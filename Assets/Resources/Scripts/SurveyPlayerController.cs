using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyPlayerController : MonoBehaviour {
    public Transform TrackingSpace;
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 rightPosition = TrackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
        Vector3 leftPosition = TrackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
	}
}
