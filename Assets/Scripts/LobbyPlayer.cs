using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LobbyPlayer : NetworkLobbyPlayer {

    private bool time = false;
    private float elapsedTime = 0f;
    public float GazeTimeLimit = 5f;
    
	void Update() {
	    if (time) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > GazeTimeLimit)
            {
                print("player ready: " + this.playerControllerId);
                //readyToBegin = true;
                SendReadyToBeginMessage();
                StopTimer();
            }
        }
	}

    public void StartTimer()
    {
        if (!time && !readyToBegin)
        {
            elapsedTime = 0f;
            time = true;
        }
    }

    public void StopTimer()
    {
        if (time)
        {
            time = false;
            elapsedTime = 0f;
        }
    }
}
