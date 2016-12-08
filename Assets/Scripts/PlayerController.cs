using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    private Color chosenColor = Color.white;

    [SyncVar]
    private bool winner = false;

    public float speed = 3.0f;

    private Rigidbody rb;

    public Vector3 offset;

    private GvrHead head;

    private float elapsedTime = 0.0f;

    private bool deathCubeDead = false;
	private bool playerIsViewer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Setting color " + chosenColor);
        foreach (Material mat in gameObject.GetComponent<MeshRenderer>().materials)
            mat.color = chosenColor;

        if (!isLocalPlayer)
            return;

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

    public override void OnStartServer()
    {
        chosenColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void Update()
    {
        if (winner)
        {
            rb.isKinematic = true;

			// Start Win Fade Out. No Fade in necessary? Or is it when back in the lobby
			// Maybe add a wait manually? For both win and lose!
			if (!deathCubeDead) {
				// Activate Death Cube fade
				deathcube[] dcs = (deathcube[])FindObjectsOfType (typeof(deathcube));
				if (dcs.Length > 0) {
					foreach (deathcube deathcube in dcs) {
						//Debug.Log("Activated!");
						deathCubeDead = true;
						deathcube.SetWin();
					}
				}
			}
            return;
        }

        // Check that we have not placed the player at the top to watch the game
        if (!playerIsViewer)
        {
            if (rb.transform.position.y > 2f)
                rb.angularDrag = 2.0f;
            else
                rb.angularDrag = 0.2f;

            // Give user time to face in the right direction
            elapsedTime += Time.deltaTime;
            if (elapsedTime < 3f)
            {
                rb.isKinematic = true;
            }
            else
            {
                rb.isKinematic = false;
            }
        }

        if (!isLocalPlayer)
            return;

		// Check if player has fallen off of platform
        if ((transform.position.y <= -5.0f) & (!deathCubeDead))
        {
            // Activate Death Cube fade
            deathcube[] dcs = (deathcube[])FindObjectsOfType(typeof(deathcube));
            if (dcs.Length > 0)
            {
                foreach (deathcube deathcube in dcs)
                {
                    //Debug.Log("Activated!");
                    deathCubeDead = true;
                    deathcube.StartFadeOut();
					deathcube.SetLose ();
                }
            }

        }

        if ((transform.position.y < -30.0f) & (!playerIsViewer))
        {
            rb.AddTorque(Vector3.zero);
            rb.isKinematic = true;

            playerIsViewer = true;

            // Activate Death Cube fade
            deathcube[] dcs = (deathcube[])FindObjectsOfType(typeof(deathcube));
            if (dcs.Length > 0)
            {
                foreach (deathcube deathcube in dcs)
                {
					// Comment out because fade in logic has been moved to deathcube.
					// Current wait is 2 seconds to read you lose message
                    //deathcube.StartFadeIn();
                }
            }

			// Should only have to do this once...
            transform.position = new Vector3(0f,20.0f,0f);

            return;
        }

        //float forwardMotion = 10;// Input.GetAxis ("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        //Vector3 leftVector = Vector3.Cross (Camera.main.transform.right, Vector3.up);
        //Debug.DrawRay (transform.position, leftVector * 10, Color.blue);
        //rb.AddTorque (head.Gaze.direction * 200f * Time.deltaTime);
        if ((rb.transform.position.y < 3f) && (rb.velocity.magnitude < speed))
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

    public void SetWinner()
    {

		if (!isServer)
			return;

		winner = true;

    }

}