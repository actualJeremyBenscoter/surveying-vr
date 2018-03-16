using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	//==============================================================
	// Spawnpoint for the bullet
	//==============================================================
	private float spawnTime;

	void Start ()
	{
		spawnTime = Time.time;
	}

	void Update ()
	{
		//==============================================================
		// No collision detected! Destroy bullet gameObject after 5 seconds
		//==============================================================
		if (Time.time >= spawnTime + 5)
		{
			//==============================================================
			// Destroy bullet gameObject
			//==============================================================
			destroyBullet ();
			Debug.Log ("Miss and Self Destruct!");
		}
	}

	//==============================================================
	// Destroy bullet gameObject if collision is detected
	//==============================================================
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.name != "Player")
		{
			//==============================================================
			// Destroy bullet gameObject
			//==============================================================
			destroyBullet ();
			Debug.Log ("Hit target " + collision.collider.gameObject.name);
		}
	}

	//==============================================================
	// Destroy bullet gameObject
	//==============================================================
	void destroyBullet ()
	{
		gameObject.SetActive(false);
		Destroy(gameObject);
	}
}
