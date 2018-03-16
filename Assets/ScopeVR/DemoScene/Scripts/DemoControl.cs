using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DemoControl : MonoBehaviour
{
	public Material Day;
	public Material Night;
	public Light dayLight;
	private Light[] BallonLight;
	private bool swapDayNight;

	void Start ()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () 
	{
		//==============================================================
		// F1 Toggle day/night
		//==============================================================
		if (Input.GetKeyDown(KeyCode.F1))
		{
			swapDayNight = !swapDayNight;

			if (swapDayNight) 
			{
				//==============================================================
				// Turn off every pointlight in scene
				//==============================================================
				BallonLight = FindObjectsOfType(typeof(Light)) as Light[];
				foreach(Light light in BallonLight)
				{
					light.intensity = 0;
				}
				//==============================================================
				// Turn off directional light
				//==============================================================
				dayLight.enabled = false;
				RenderSettings.skybox = Night;
			}
			else
			{
				//==============================================================
				// Turn on every pointlight in scene
				//==============================================================
				BallonLight = FindObjectsOfType (typeof(Light)) as Light[];
				foreach (Light light in BallonLight) {
					light.intensity = 1;
				}
				//==============================================================
				// Turn on directional light
				//==============================================================
				dayLight.enabled = true;
				RenderSettings.skybox = Day;
			} 

		}

		//==============================================================
		// ESC - Exit
		//==============================================================
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit ();
		}
	}
}
