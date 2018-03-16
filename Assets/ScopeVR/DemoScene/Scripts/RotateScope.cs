using UnityEngine;
using System.Collections;

public class RotateScope : MonoBehaviour 
{
	//==============================================================
	// Mousebuttons
	//==============================================================
	const int LEFTCLICK = 0;
	const int RIGHTCLICK = 1;

	//==============================================================
	// Game Transforms
	//==============================================================
	public Transform Scope; // The Frisbee

	//==============================================================
	// Tilt and pan the disc with right mouse button pressed
	//==============================================================
	private float xTilt = 0.0f; // Up , down
	private float yTilt = 0.0f; // Left, right

	private Quaternion tiltCurrentRotation;
	private Quaternion tiltDesiredRotation;

	public int xTiltMin = -25;
	public int xTiltMax = 25;
	public int yTiltMin = -40;
	public int yTiltMax = 40;
	private float tiltSpeed = 200.0f;
	private float zoomDampening = 10.0f;

	//==============================================================
	// LateUpdate Tilt and pan disc
	//==============================================================
	void LateUpdate ()
	{
		if (Input.GetMouseButton (RIGHTCLICK))
			ScopeRotate();
	}

	//==============================================================
	// Rotate Scope Z and Y
	//==============================================================

	private void ScopeRotate ()
	{
		xTilt += Input.GetAxis ("Mouse Y") * tiltSpeed * 0.02f;
		xTilt = ClampAngle (xTilt, xTiltMin, xTiltMax);

		yTilt -= Input.GetAxis ("Mouse X") * tiltSpeed * 0.02f;
		yTilt = ClampAngle (yTilt, yTiltMin, yTiltMax);

		tiltDesiredRotation = Quaternion.Euler (xTilt, yTilt, 0);
		tiltCurrentRotation = Scope.localRotation;
		Scope.localRotation = Quaternion.Lerp (tiltCurrentRotation, tiltDesiredRotation, Time.deltaTime * zoomDampening);
	}

	private float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp (angle, min, max);
	}
}