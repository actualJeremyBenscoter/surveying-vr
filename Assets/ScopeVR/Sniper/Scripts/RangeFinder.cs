using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangeFinder : MonoBehaviour 
{
	//==============================================================
	// Variable for the Range Camera component
	//==============================================================
	[Header("Range Camera")]
	public Camera rangeCamera;

	public static int zoomFactor;

	//private Text osdText;
	private TextMesh osdText;
	private Vector3 cameraPosition;
	private float distance;
	private float timeleft = 0.0f;	// Left time for current update interval
	private float updateInterval = 0.1f;

	public enum unit 
	{
		meter, yards
	}
	public unit RangeUnit;

	//==============================================================
	// At Start, get the Text component and position of the camera
	//==============================================================
	void Start()
	{
		osdText = GetComponent<TextMesh>();
		cameraPosition = rangeCamera.transform.position;
		timeleft = updateInterval;
	}

	void Update() 
	{
		timeleft -= Time.deltaTime;
		RaycastHit hit;
		//==============================================================
		// Interval ended - update GUI text and start new interval
		//==============================================================
		if( timeleft <= 0.0 )
		{
			if (Physics.Raycast (rangeCamera.ScreenPointToRay (new Vector3 (Screen.width / 2f, Screen.height / 2f, 0f)), out hit)) 
			{
				cameraPosition = rangeCamera.transform.position;
				distance = Vector3.Distance (cameraPosition, hit.point);

				if (RangeUnit == 0)  
				{
					osdText.text = "RNG " + (distance).ToString ("f2") + " m" + "\n\n" + "ZOOM x" + zoomFactor;
				}
				else
				{
					osdText.text = "RNG " + (distance*1.0936133).ToString ("f2") + " yd" + "\n\n" + "ZOOM x" + zoomFactor;
				} 
			} 
			else 
			{
				osdText.text = "RNG INF" + "\n\n" + "ZOOM x" + zoomFactor;
			}
			timeleft = updateInterval;
		}
	}
}
