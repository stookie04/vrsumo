using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;
    private Vector3 offset;

    void Start ()
	{
		rb = GetComponent<Rigidbody>();
        offset = Camera.main.transform.position - transform.position;
    }

    void FixedUpdate ()
	{
		float forwardMotion = Input.GetAxis ("Vertical");
		float rotation = Input.GetAxis ("Horizontal");

		Vector3 leftVector = Vector3.Cross (Camera.main.transform.right, Vector3.up);
		//Debug.DrawRay (transform.position, leftVector * 10, Color.blue);
		rb.AddTorque (Camera.main.transform.right * forwardMotion * speed);
        //rb.AddForce (Camera.main.transform.forward * forwardMotion * speed);
        transform.Rotate(0.0f, rotation*speed, 0.0f);
        updateCamera(rotation);
	}

    void updateCamera(float rotation)
    {
        Camera.main.transform.position = transform.position + offset;
        Camera.main.transform.Rotate(0.0f, rotation*speed, 0.0f);
    }
}