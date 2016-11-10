using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

    public Vector3 offset = new Vector3(0f, 0.5f, -1f);

    void Start ()
	{
		rb = GetComponent<Rigidbody>();
        //transform.position = Camera.main.transform.position;
        //offset = Camera.main.transform.position - transform.position;

        GameObject head = GameObject.Find("head");
        if (head)
        {
            print("found head");
            head.GetComponent<GvrHead>().target = transform;
        }
    }

    void FixedUpdate ()
	{
		float forwardMotion = Input.GetAxis ("Vertical");
		float rotation = Input.GetAxis ("Horizontal");

		//Vector3 leftVector = Vector3.Cross (Camera.main.transform.right, Vector3.up);
		//Debug.DrawRay (transform.position, leftVector * 10, Color.blue);
		rb.AddTorque (Camera.main.transform.right * forwardMotion * speed);
        //rb.AddForce (Camera.main.transform.forward * forwardMotion * speed);
        transform.Rotate(0.0f, rotation*speed, 0.0f);
        //updateCamera(rotation);
	}

    void updateCamera(float rotation)
    {
        Camera.main.transform.position = transform.position + offset;
        //Camera.main.transform.Rotate(0.0f, -rotation*speed, 0.0f);
    }
}