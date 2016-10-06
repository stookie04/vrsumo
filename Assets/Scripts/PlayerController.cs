using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float forwardMotion = Input.GetAxis ("Vertical");
		float rotation = Input.GetAxis ("Horizontal");

		Vector3 leftVector = Vector3.Cross (Camera.main.transform.right, Vector3.up);
		Debug.DrawRay (transform.position, leftVector * 10, Color.blue);
		rb.AddTorque (Camera.main.transform.right * forwardMotion * speed);
		//rb.AddForce (Camera.main.transform.forward * forwardMotion * speed);
		transform.Rotate (transform.InverseTransformDirection(Vector3.up), rotation * speed);
	}
}