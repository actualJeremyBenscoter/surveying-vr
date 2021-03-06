using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{	
	private float random;

	private void Start()
	{
		//==============================================================
		// Get random number between 0.5 - 1.5
		//==============================================================
		random = Random.Range(0.5f, 1.5f);
	}

	void Update () 
	{
		//==============================================================
		// Rotate the targets
		//==============================================================
		transform.Rotate (new Vector3 (0, 90, 0) * Time.deltaTime * random);
	}
	//==============================================================
	// Destroy target if collision is detected
	//==============================================================
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.name != "Player")
		{
			gameObject.SetActive (false);
			Destroy (gameObject);
		}
	}
}
