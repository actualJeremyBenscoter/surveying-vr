using UnityEngine;
using System.Collections;

public class MouseLookOrbit : MonoBehaviour
{
	//==============================================================
	// Mousebuttons
	//==============================================================
	const int LEFTCLICK = 0;
	const int RIGHTCLICK = 1;

	private Transform MyCam;
	private float xDeg = 0.0f;
	private float yDeg = 2.5f;
	private Quaternion currentRotation;
	private Quaternion desiredRotation;
	//private Quaternion startingRotation;

	public int yMinLimit = -60;
	public int yMaxLimit = 60;
	public static float xSpeed = 200.0f;
	public static float ySpeed = 200.0f;
	public float zoomDampening = 10.0f;

	private Quaternion rotation;

	void Start()
	{
		MyCam = transform;
	}

	void LateUpdate ()
	{
		if (!Input.GetMouseButton (RIGHTCLICK))
		{
			FreeLook ();
		}

		if (Input.GetKey(KeyCode.Escape))	// Show Mousecursor
		{
			Cursor.visible = true;
		}
	}

	private void FreeLook ()
	{
		// Orbiting camera
		xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
		yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

		//Clamp the vertical axis for the orbit
		yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

		// set camera rotation
		desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
		currentRotation = MyCam.localRotation;
		rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
		MyCam.localRotation = rotation;
	}

	private static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp (angle, min, max);
	}
}