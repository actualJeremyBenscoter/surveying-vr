using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControl : MonoBehaviour
{
	//==============================================================
	// Public variables
	//==============================================================
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 1.0f;

	//==============================================================
	// Private variables
	//==============================================================
	private Vector3 targetVelocity;
	private float accelerate;
	private bool grounded = false;

	//==============================================================
	// Get Rigidbody
	//==============================================================
	void Awake () 
	{
		GetComponent<Rigidbody>().freezeRotation = true;
		GetComponent<Rigidbody>().useGravity = false;
	}

	void FixedUpdate ()
	{
		if (grounded)
		{
			//==============================================================
			// Standard Inputkeys
			//==============================================================
			targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

			//==============================================================
			// Calculate how fast we should be moving
			//==============================================================
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;

			//==============================================================
			// Apply a force that attempts to reach our target velocity
			//==============================================================
			Vector3 velocity = GetComponent<Rigidbody>().velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0.0f;
			GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

			//==============================================================
			// Jump
			//==============================================================
			if (canJump && Input.GetButton("Jump"))
			{
				GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}

		//==============================================================
		// We apply gravity manually for more tuning control
		//==============================================================
		GetComponent<Rigidbody>().AddForce(new Vector3 (0.0f, -gravity * GetComponent<Rigidbody>().mass, 0.0f));

		grounded = false;
	}

	//==============================================================
	// Grounded
	//==============================================================
	void OnCollisionStay ()
	{
		grounded = true;    
	}

	//==============================================================
	// Jump
	//==============================================================
	float CalculateJumpVerticalSpeed ()
	{
		//==============================================================
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		//==============================================================
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}