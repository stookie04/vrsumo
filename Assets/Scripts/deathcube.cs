using UnityEngine;
using System.Collections;

public class deathcube : MonoBehaviour {

	float timeToFade = 1.0f;
	float timeForMessage = 1.5f;
	//float elapsedTime = 0f;

	bool fadein = false;
	bool fadeout = false;
	bool loseMessage = false;
	bool winMessage = false;
	public Material youLose;
	public Material youWin;
	public Material black;
	float alpha = 0.0f;
	public bool fadeoutComplete = false;

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

	public void SetLose(){
		loseMessage = true;
	}

	public void SetWin(){
		StartFadeOut();
		winMessage = true;
	}
	
	// Update is called once per frame
	void Update () {

		//Color color = GetComponent<Renderer> ().material.color;

		if ((alpha < 1.0f) & (fadeout)){
			alpha = Mathf.Min(1.0f, alpha + (Time.deltaTime / timeToFade));
			Color newColor = new Color (0.0f, 0.0f, 0.0f, alpha);
			GetComponent<Renderer> ().material.color = newColor;
		}
		//Display you lose symbol before fading back in.
		if ((alpha >= 1.0f) & (loseMessage)){
			fadeout = false; // fadeout is complete
			alpha = 1.0f; // clamp to alpha of 1.0
			GetComponent<Renderer> ().material = youLose;
			loseMessage = false; //only run this function once
			fadeoutComplete = true;
		}
		//Display you win message before fading back in.
		if ((alpha >= 1.0f) & (winMessage)){
			fadeout = false; // fadeout is complete
			alpha = 1.0f; // clamp to alpha of 1.0
			GetComponent<Renderer> ().material = youWin;
			winMessage = false;
			fadeoutComplete = true;
		}
			
		//Debug.Log (timeForMessage);
		// Implement wait time for reader to read win/lose message
		if ((alpha >= 1.0f) & (timeForMessage > 0.0f) & (fadeoutComplete)) {
			timeForMessage = timeForMessage - Time.deltaTime;
		}
		// In the lose scenario this might be redundant.
		// Comment out fade back in logic from Player Controller.
		if ((alpha >= 1.0f) & (timeForMessage <= 0.0f) & (!fadein)) {
			StartFadeIn();
			GetComponent<Renderer> ().material = black;
		}


		//begin fading back in as viewer
		if ((alpha > 0.0f) & (fadein)){
			alpha = Mathf.Max (0.0f, alpha - (Time.deltaTime / timeToFade));
			Color newColor = new Color (0.0f, 0.0f, 0.0f, alpha);
			GetComponent<Renderer> ().material.color = newColor;
		}
		//fade in over
		if ((alpha <= 0.0f) & (fadein)){
			fadein = false;
			alpha = 0.0f; //clamp to alpha of zero.
		}
	}
}

// elapsedtime/time * 1.0
