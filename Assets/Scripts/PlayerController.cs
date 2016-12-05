using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{

    public float speed = 3.0f;

    private Rigidbody rb;

    public Vector3 offset;

    private GvrHead head;

    private float elapsedTime = 0.0f;

    private bool deathCubeDead = false;
	private bool playerIsViewer = false;

    void Start()
    {
        if (!isLocalPlayer)
            return;

        rb = GetComponent<Rigidbody>();
        //transform.position = Camera.main.transform.position;
        //offset = Camera.main.transform.position - transform.position;

        head = (GvrHead)FindObjectOfType(typeof(GvrHead));
        head.transform.position = transform.position + offset;
        GvrViewer viewer = (GvrViewer)FindObjectOfType(typeof(GvrViewer));
        viewer.Recenter();
        //head.transform.rotation = Quaternion.LookRotation(Vector3.zero - head.transform.position);
        //Camera.main.transform.position = transform.position + offset;
        //Camera.main.transform.rotation = Quaternion.LookRotation(Vector3.zero - Camera.main.transform.position);
    }

    void Update()
    {

        if (!isLocalPlayer)
            return;

        if (rb.transform.position.y > 2f)
            rb.angularDrag = 2.0f;
        else
            rb.angularDrag = 0.2f;

        // Give user time to face in the right direction
        elapsedTime += Time.deltaTime;
        if (elapsedTime < 3f)
        {
            rb.isKinematic = true;
            return;
        }
        else
        {
            rb.isKinematic = false;
        }

		if ((transform.position.y <= -5.0f) & (!deathCubeDead))
        {
            // Activate Death Cube fade
            deathcube[] dcs = (deathcube[])FindObjectsOfType(typeof(deathcube));
			if (dcs.Length > 0) {
				foreach (deathcube deathcube in dcs) {
					//Debug.Log("Activated!");
					deathCubeDead = true;
					deathcube.StartFadeOut ();
				}
			}
        }


		if ((transform.position.y < -30.0f) & (!playerIsViewer))
        {
            rb.AddTorque(Vector3.zero);
            rb.isKinematic = true;

			// Activate Death Cube fade
			deathcube[] dcs = (deathcube[])FindObjectsOfType(typeof(deathcube));
			if (dcs.Length > 0) {
				foreach (deathcube deathcube in dcs) {
					//Debug.Log("Activated!");
					playerIsViewer = true;
					deathcube.StartFadeIn();
				}
			}

            return;
        }

        //float forwardMotion = 10;// Input.GetAxis ("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        //Vector3 leftVector = Vector3.Cross (Camera.main.transform.right, Vector3.up);
        //Debug.DrawRay (transform.position, leftVector * 10, Color.blue);
        //rb.AddTorque (head.Gaze.direction * 200f * Time.deltaTime);
        if (rb.velocity.magnitude < speed)
        {
            rb.AddForce(head.Gaze.direction * 1000 * Time.deltaTime);
        }
        updateCamera(rotation);
    }

    void updateCamera(float rotation)
    {
        if (head)
        {
            head.transform.position = transform.position + offset;
        }
        //Camera.main.transform.position = transform.position + offset; 
        //Camera.main.transform.Rotate(0.0f, -rotation*speed, 0.0f);
    }
}