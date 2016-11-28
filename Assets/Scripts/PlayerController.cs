using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	public float speed;

	private Rigidbody rb;

    public Vector3 offset;

    private GvrHead head;

    void Start ()
	{
		rb = GetComponent<Rigidbody>();
        //transform.position = Camera.main.transform.position;
        //offset = Camera.main.transform.position - transform.position;

        head = (GvrHead)FindObjectOfType(typeof(GvrHead));
        head.transform.position = transform.position + offset;
        head.transform.rotation = Quaternion.LookRotation(Vector3.zero - head.transform.position);
        //Camera.main.transform.position = transform.position + offset;
        //Camera.main.transform.rotation = Quaternion.LookRotation(Vector3.zero - Camera.main.transform.position);
    }

    void FixedUpdate ()
	{
        if (!isLocalPlayer)
            return;

        if (transform.position.y < 0.0f)
        {
            rb.AddTorque(Vector3.zero);
            rb.isKinematic = true;
            return;
        }

        float forwardMotion = Input.GetAxis ("Vertical");
        float rotation = Input.GetAxis ("Horizontal");

		//Vector3 leftVector = Vector3.Cross (Camera.main.transform.right, Vector3.up);
		//Debug.DrawRay (transform.position, leftVector * 10, Color.blue);
		rb.AddTorque (head.transform.right * forwardMotion * speed);
        //rb.AddForce (Camera.main.transform.forward * forwardMotion * speed);
        transform.Rotate(0.0f, rotation*speed, 0.0f);
        updateCamera(rotation);
	}

    void updateCamera(float rotation)
    {
        if (head)
            head.transform.position = transform.position + offset;
        //Camera.main.transform.position = transform.position + offset; 
        //Camera.main.transform.Rotate(0.0f, -rotation*speed, 0.0f);
    }
}