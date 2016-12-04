using UnityEngine;
using Gvr;
using System.Collections;

public class fall : MonoBehaviour {

    float fallTime;
    float elapsedTime = 0f;
    bool rampsActive = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        if (tag == "Outer")
            fallTime = 20.0f;
        else if (tag == "Mid")
            fallTime = 35.0f;

	    if (elapsedTime > fallTime && transform.position.y > -25.0)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

        if (transform.position.y <= -25.0)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && rampsActive)
        {
            rampsActive = false;
            GameObject[] ramps = GameObject.FindGameObjectsWithTag("ramp");
            foreach (GameObject go in ramps)
            {
                go.SetActive(false);
            }
            //GvrHead head = (GvrHead)FindObjectOfType(typeof(GvrHead));
            //if (head)
            //{
                //head.trackRotation = true;
            //}
        }
    }
}
