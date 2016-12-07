using UnityEngine;
using System.Collections;

public class deathcube : MonoBehaviour {

	float timeToFade = 1.0f;
	//float elapsedTime = 0f;

	bool fadein = false;
	bool fadeout = false;

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
	public void StartFadeOut(){
		fadeout = true;
	}

	public void StartFadeIn(){
		fadein = true;
	}
	
	// Update is called once per frame
	void Update () {
		Renderer rend = GetComponent<Renderer> ();

		Color color = GetComponent<Renderer> ().material.color;

		if ((color.a < 1.0f) & (fadeout)){
			Color newColor = new Color (color.r, color.g, color.b, Mathf.Min (1.0f, color.a + (Time.deltaTime / timeToFade)));
			GetComponent<Renderer> ().material.color = newColor;
		}

		if ((color.a > 0.0f) & (fadein)){
			Color newColor = new Color (color.r, color.g, color.b, Mathf.Max (0.0f, color.a - (Time.deltaTime / timeToFade)));
			GetComponent<Renderer> ().material.color = newColor;
		}
	
	}
}

// elapsedtime/time * 1.0
