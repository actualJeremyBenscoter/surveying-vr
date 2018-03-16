using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//==============================================================
// A SniperScope using a second camera
//==============================================================

[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (AudioClip))]

public class SniperScope : MonoBehaviour
{
	[Header("Audio")]
	//==============================================================
	// Audio
	//==============================================================
	public AudioSource AudioSource;
	public AudioClip Zoom;
	public AudioClip Swap;

	private float ZoomVolume = 1.0f;
	private float SwapVolume = 1.0f;

	//==============================================================
	// private variable for the Sniper Camera component
	//==============================================================
	private Camera sniperCam;

	//==============================================================
	// creates a value to raise and lower the camera's field of view
	//==============================================================
	private int zoomX;

	//==============================================================
	// creates a value which describes the camera's zoom multiplier
	//==============================================================
	private int zoomFactor;

	//==============================================================
	// Default FOV value
	//==============================================================
	private float defaultFov = 60f;

	//==============================================================
	// Using Lerp to zoom softly in and out Enable/Disable
	//==============================================================
	[Header("Lerp zoom enable/disable")]
	public bool softZoom = true;

	//==============================================================
	// Audio Enable/Disable
	//==============================================================
	[Header("Audio")]
	public bool soundEffects = true;

	//==============================================================
	// Zoom Factor. Different FOV for 0x, 2x, 4x, 6x, 8x, 10x zoom
	//==============================================================
	private float[] zoomFov = {60,30,15,8,4,1};

	//==============================================================
	//LERP Variables for the Soft Zoom function
	//==============================================================
	private float timeDuringZooming = 0.2f;
	private float timeStartLerp;
	private float timeSinceStarted;
	private float zoomComplete;
	private float currentZoom;
	private bool isLerping;

	//==============================================================
	// NightVision Enable/Disable
	//==============================================================
	private bool nightVision = false;

	//==============================================================
	// At Start, get the Camera component
	//==============================================================
	void Start()
	{
		currentZoom = defaultFov;
		sniperCam = GetComponent<Camera> ();
		sniperCam.fieldOfView = defaultFov;
	}

	void LateUpdate()
	{
		//==============================================================
		// Attaches the float y to scrollwheel up or down
		//==============================================================
		float mouseScroll = Input.mouseScrollDelta.y;

		//==============================================================
		// If the wheel goes up it or Key "+" is pressed, increment 1 to array "zoomX"
		//==============================================================
		if (mouseScroll > 0 || Input.GetKeyDown (KeyCode.Plus) || Input.GetKeyDown (KeyCode.KeypadPlus)) 
		{
			//==============================================================
			// If not Zoom with Lerp is active
			//==============================================================
			if (!isLerping)
			{
				zoomX += 1;
				if (zoomX <= 5)
				{
					//==============================================================
					// Used in the OSD "ZOOM x.."
					//==============================================================
					RangeFinder.zoomFactor += 2;

					//==============================================================
					// Zoom in
					//==============================================================
					zoomInOut ();
				} 
				else
					zoomX = 5;
			}
		}

		//==============================================================
		// If the wheel goes down or Key "-" is pressed, decrement 1 to array "zoomX"
		//==============================================================
		else if (mouseScroll < -0 || Input.GetKeyDown (KeyCode.Minus) || Input.GetKeyDown (KeyCode.KeypadMinus)) 
		{
			//==============================================================
			// If not Zoom with Lerp is active
			//==============================================================
			if (!isLerping) 
			{
				zoomX -= 1;
				if (zoomX >= 0) 
				{
					//==============================================================
					//  Used in the OSD "ZOOM x.."
					//==============================================================
					RangeFinder.zoomFactor -= 2;

					//==============================================================
					// Zoom out
					//==============================================================
					zoomInOut ();
				} 
				else
					zoomX = 0;
			}
		}

		//==============================================================
		// Key N or RightMouseButton. NightVision on/off
		//==============================================================
		if (Input.GetKeyDown (KeyCode.N) || Input.GetMouseButtonDown(2)) 
		{
			MonoBehaviour NightVisionScript = GetComponent<NightVision>();
			if (nightVision) 
			{
				NightVisionScript.enabled = false;
				nightVision = false;
			} 
			else 
			{
				NightVisionScript.enabled = true;
				nightVision = true;
			}
			//==============================================================
			// Play ClickSound
			//==============================================================
			if(soundEffects)
				AudioSource.PlayOneShot (Swap, SwapVolume);
		}
	}

	private void zoomInOut()
	{
		//==============================================================
		// Play ClickSound
		//==============================================================
		if (soundEffects)
			AudioSource.PlayOneShot (Zoom, ZoomVolume);

		//==============================================================
		// Makes the actual change to Field Of View
		//==============================================================
		if (softZoom) 
			// Soft Zoom with Lerp
			StartCoroutine ("lerpZoom", zoomFov[zoomX]);
		else
			// Fast Zoom
			sniperCam.fieldOfView = zoomFov[zoomX];

		//==============================================================
		// Note! This is for ajusting Mouselook speed while zooming
		// You'll have to code this with your own mouselook class
		//==============================================================
		MouseLookOrbit.xSpeed = zoomFov[zoomX];
		MouseLookOrbit.ySpeed = zoomFov[zoomX];
		if (zoomFov[zoomX] == defaultFov) 
		{
			MouseLookOrbit.xSpeed = 200;
			MouseLookOrbit.ySpeed = 200;	
		}
	}

	//==============================================================
	// Soft Zoom "Lerp"
	//==============================================================
	IEnumerator lerpZoom (float newZoom) 
	{
		// Lerping zoom is true
		isLerping = true;
		// Reset time
		timeStartLerp = Time.time;
		// Reset LERP Complete
		zoomComplete = 0f;

		while (zoomComplete < 1.0f) 
		{
			timeSinceStarted = Time.time - timeStartLerp;
			zoomComplete = timeSinceStarted / timeDuringZooming;
			sniperCam.fieldOfView = Mathf.Lerp (currentZoom, newZoom, zoomComplete);
			yield return null;
		}
		currentZoom = newZoom;
		isLerping = false;
	}
}