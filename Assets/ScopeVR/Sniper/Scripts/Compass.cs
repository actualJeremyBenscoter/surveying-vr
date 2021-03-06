using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//==============================================================
// A Compass attached to the scope
//==============================================================

[RequireComponent (typeof (Transform))]
[RequireComponent (typeof (GameObject))]

public class Compass : MonoBehaviour
{

	[Header("Transform and Game Objects")]
	//==============================================================
	// Transform variable to be populated with the SniperScope object
	//==============================================================
	public Transform sniperScope;

	//==============================================================
	// Gameobject variable for the element representing the Compass
	//==============================================================
	public GameObject compass;

	//==============================================================
	// private variable for the compass
	//==============================================================
	private Vector3 compassAngle;

	void LateUpdate()
	{
		//==============================================================
		// Set SniperScope's rotation on Y-axis as the Compass rotation on Z-axis
		//==============================================================
		compassAngle = new Vector3 (compass.transform.eulerAngles.x, compass.transform.eulerAngles.y, sniperScope.transform.eulerAngles.y);

		//==============================================================
		// Rotate the Compass
		//==============================================================
		compass.transform.eulerAngles = compassAngle;
	}
}