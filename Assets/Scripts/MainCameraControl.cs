using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControl : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	private Vector3 lastLook;

	void Start ()
	{
		offset = transform.position - player.transform.position;
		lastLook = player.transform.forward;
	}

	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
		//transform.LookAt (player.transform.forward - Camera.main.transform.forward);
		//Vector3 newLook = transform.TransformDirection (player.transform.forward);
		Vector3 newLook = Vector3.Cross(player.transform.right, Vector3.up);
		Debug.DrawRay (player.transform.position, newLook * 10, Color.green); 
		transform.rotation = Quaternion.LookRotation (newLook);
		Debug.DrawRay (player.transform.position, transform.forward * 10, Color.red); 
	}
}
