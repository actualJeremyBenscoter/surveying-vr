using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Transform))]
[RequireComponent (typeof (AudioClip))]
public class Shoot : MonoBehaviour 
{
	//==============================================================
	// Declaring RigidBody and spawnpoint for the Projectile (Bullet)
	//==============================================================
	public Rigidbody projectile;
	public Transform spawnPoint;
	public AudioSource AudioSource;
	public AudioClip sniperShot;
	private float sniperShotVolume = 1.0f;

	//==============================================================
	// Bullet speed
	//==============================================================
	public float bulletSpeed;

	void Update () 
	{
		//==============================================================
		// Left mousebutton pressed
		//==============================================================
		if(Input.GetButtonDown("Fire1"))
		{
			//==============================================================
			// Play audio "SniperShot"
			//==============================================================
			AudioSource.PlayOneShot (sniperShot, sniperShotVolume);
			//==============================================================
			// Declaring RigidBody "Clone"
			//==============================================================
			Rigidbody clone;
			//==============================================================
			// Clone and spawn a bullet
			//==============================================================
			clone = (Rigidbody)Instantiate(projectile, spawnPoint.position, projectile.rotation);
			//==============================================================
			// Set speed to the Bullet
			//==============================================================
			clone.velocity = spawnPoint.TransformDirection (Vector3.forward*bulletSpeed);
		}
	}
}