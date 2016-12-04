﻿using UnityEngine;
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
            fallTime = 10.0f;
        else if (tag == "Mid")
            fallTime = 20.0f;

	    if (elapsedTime > fallTime && transform.position.y > -25.0)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

		if (transform.position.y <= -10.0) {
			
			// Activate Death Cube fade
			GameObject[] dcs = GameObject.FindGameObjectsWithTag ("deathcube");
			foreach (GameObject deathcube in dcs)
			{
				deathcube.SetActive(false);
			}
		}

        if (transform.position.y <= -25.0)
        {
			// Destroy game object
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
