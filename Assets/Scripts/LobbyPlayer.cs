using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LobbyPlayer : NetworkLobbyPlayer {

    public float GazeTimeLimit = 5f;

    private bool time = false;
    private float elapsedTime = 0f;
    private bool ready = false;
    private Button readyButton;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        ready = false;
        time = false;
        elapsedTime = 0f;
        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));
        if (canvas)
        {
            readyButton = canvas.GetComponentInChildren<Button>();
            if (readyButton)
            {
                readyButton.GetComponentInChildren<Text>().text = "Ready?";
                CheckReadiness.lobbyPlayer = this;
            }
        }
    }

	void Update()
    {
        if (!isLocalPlayer)
            return;

	    if (time && !ready)
        {
            elapsedTime += Time.deltaTime;
            if (readyButton)
                readyButton.GetComponentInChildren<Text>().text = "Ready in " + (int)(GazeTimeLimit-elapsedTime) + "...";
            if (elapsedTime > GazeTimeLimit)
            {
                ready = true;
                if (isLocalPlayer)
                    SendReadyToBeginMessage();
                StopTimer();
                if (readyButton)
                    readyButton.GetComponentInChildren<Text>().text = "Starting";
            }
        }
	}

    public void StartTimer()
    {
        if (!isLocalPlayer)
            return;

        if (!time && !ready)
        {
            elapsedTime = 0f;
            time = true;
            if (readyButton)
                readyButton.GetComponentInChildren<Text>().text = "Ready in " + (int)(GazeTimeLimit) + "...";
        }
    }

    public void StopTimer()
    {
        if (!isLocalPlayer)
            return;

        if (time)
        {
            time = false;
            elapsedTime = 0f;
            if (readyButton)
                readyButton.GetComponentInChildren<Text>().text = "Ready?";
        }
    }

    public override void OnClientEnterLobby()
    {
        if (!isLocalPlayer)
            return;

        base.OnClientEnterLobby();
        SendNotReadyToBeginMessage();
        time = false;
        elapsedTime = 0f;
        ready = false;
        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));
        if (canvas)
        {
            readyButton = canvas.GetComponentInChildren<Button>();
            if (readyButton)
                readyButton.GetComponentInChildren<Text>().text = "Ready?";
        }
    }
    
}
