using UnityEngine;
using System.Collections;

public class fall : MonoBehaviour {

    float fallTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (tag == "Outer")
            fallTime = 10.0f;
        else if (tag == "Mid")
            fallTime = 20.0f;

	    if (Time.time > fallTime && transform.position.y > -25.0)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

        if (transform.position.y <= -25.0)
        {
            Destroy(gameObject);
        }
	}
}
