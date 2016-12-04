using UnityEngine;
using System.Collections;

public class deathcube : MonoBehaviour {

	float timeToFade = 2.0f;
	//float elapsedTime = 0f;

	bool fade = false;

	// Use this for do once at start behaviors
	void Activate(){
		
	}

	// Use this for initialization
	void Start () {
		Color color = GetComponent<Renderer>().material.color;
		color.a = 0.0f;
		GetComponent<Renderer> ().material.color = color;
	}

	//Start Color Fade
	public void StartColorFade(){
		fade = true;
	}
	
	// Update is called once per frame
	void Update () {
		Renderer rend = GetComponent<Renderer> ();

		Color color = GetComponent<Renderer> ().material.color;

		if ((color.a < 1.0f) & (fade)){
			Color newColor = new Color (color.r, color.g, color.b, Mathf.Min (1.0f, color.a + (Time.deltaTime / timeToFade)));
			GetComponent<Renderer> ().material.color = newColor;
		}
	
	}
}

// elapsedtime/time * 1.0
