using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;
		int layerMask = 1 << 8; // create bit mask for layer mask # 8
		// This would cast rays only against colliders in layer 8.
		// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
		layerMask = ~layerMask;
		Ray ray = new Ray(transform.position, transform.forward);
		bool didRayHit = Physics.Raycast(ray, out hitInfo, 5f, layerMask);
		if (didRayHit)
		{
			
			//print("I am a 5 unit long ray hitting " + hitInfo.transform.name);
			//hitInfo.doAnimationStuffHere;
		}
	
	}
}
