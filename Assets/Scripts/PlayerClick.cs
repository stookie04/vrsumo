using UnityEngine;
using System.Collections;

public class PlayerClick : MonoBehaviour {
	//public event Action OnClick;                                // Called when Fire1 is released and it's not a double click.
	//public event Action OnDown;                                 // Called when Fire1 is pressed.
	//public event Action OnUp;                                   // Called when Fire1 is released.
	//public event Action OnDoubleClick;                          // Called when a double click is detected.
	//public event Action OnCancel;                               // Called when Cancel is pressed.


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown){
			print (Input.inputString);
		}
	}
}