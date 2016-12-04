using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LobbyPlayer : NetworkLobbyPlayer {

    private bool time = false;
    private float elapsedTime = 0f;
    public float GazeTimeLimit = 5f;

    private Button readyButton;
    
    void Awake()
    {
        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));
        if (canvas)
        {
            readyButton = canvas.GetComponentInChildren<Button>();
            if (readyButton)
                readyButton.GetComponentInChildren<Text>().text = "Ready?";
        }
    }

	void Update() {
	    if (time) {
            elapsedTime += Time.deltaTime;
            if (isLocalPlayer && readyButton)
                readyButton.GetComponentInChildren<Text>().text = "Ready in " + (int)(GazeTimeLimit-elapsedTime) + "...";
            if (elapsedTime > GazeTimeLimit)
            {
                //readyToBegin = true;
                print("send ready");
                SendReadyToBeginMessage();
                StopTimer();
                if (isLocalPlayer && readyButton)
                    readyButton.GetComponentInChildren<Text>().text = "Starting";
            }
        }
	}

    public void StartTimer()
    {
        if (!time && !readyToBegin)
        {
            elapsedTime = 0f;
            time = true;
            if (isLocalPlayer && readyButton)
                readyButton.GetComponentInChildren<Text>().text = "Ready in " + (int)(GazeTimeLimit) + "...";
        }
    }

    public void StopTimer()
    {
        if (time)
        {
            time = false;
            elapsedTime = 0f;
            if (isLocalPlayer && readyButton)
                readyButton.GetComponentInChildren<Text>().text = "Ready?";
        }
    }
}
