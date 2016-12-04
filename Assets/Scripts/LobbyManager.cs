#define CLIENT

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LobbyManager : NetworkLobbyManager {
    
    void Awake()
    {
#if SERVER
        StartServer ();
        var canvas = FindObjectOfType<Canvas> ();
        var button = canvas.GetComponentInChildren<Button> ();
        button.GetComponentInChildren<Text> ().text = "Server";
#endif

#if CLIENT
        StartCoroutine(TryToConnect());
#endif
    }

    IEnumerator TryToConnect()
    {
        while (!this.isNetworkActive)
        {
            StartClient();
            yield return new WaitForSeconds(2f);
        }
    }

    public override void OnLobbyServerConnect(NetworkConnection conn)
    {
        base.OnLobbyServerConnect(conn);
        var canvas = FindObjectOfType<Canvas>();
        var button = canvas.GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = this.numPlayers + " Player(s)";
    }

    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        base.OnLobbyServerDisconnect(conn);
        if (this.numPlayers < 1)
        {
            StopServer();
            ServerReturnToLobby();
        }
    }
}
