using UnityEngine;
using System.Collections;

public class ScreenShotCapture : MonoBehaviour
{
	public string customPath = "D:/tmp";
	public string Timestamp = "yyyyMMddHHmmss";	// yyyyMMddhhmmssffff
	public string namePrefix = "Capture_";		// Before the timestamp // Example "Gametitle_"

	private string GetTimestamp () 
	{
		System.DateTime dateTime = System.DateTime.Now;
		return dateTime.ToString(Timestamp);
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.F10))
		{
			string fullPath = customPath + "/" + namePrefix + GetTimestamp() + ".png";
			ScreenCapture.CaptureScreenshot (fullPath);
			Debug.Log ("Capured Screenshot: "+ fullPath);
		}
	}
}