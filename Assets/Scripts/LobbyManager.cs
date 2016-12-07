#define CLIENT

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LobbyManager : NetworkLobbyManager {

    static bool awoken = false;
    bool checkAlive = false;
    float time = 0f;

    void Awake()
    {
#if SERVER
        var canvas = FindObjectOfType<Canvas>();
        var button = canvas.GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = "Server";
#endif

        if (awoken)
            return;

        awoken = true;
        checkAlive = false;
        time = 0f;
#if SERVER
        StartServer();
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
        var canvas = FindObjectOfType<Canvas>();
        var button = canvas.GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = this.numPlayers + " Player(s)";
        if (this.numPlayers < 1)
        {
            //StopServer();
            ServerReturnToLobby();
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        var canvas = FindObjectOfType<Canvas>();
        var button = canvas.GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = this.numPlayers + " Player(s)";
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
        var canvas = FindObjectOfType<Canvas>();
        var button = canvas.GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = this.numPlayers + " Player(s)";
    }

    public override void OnLobbyServerSceneChanged(string sceneName)
    {
        base.OnLobbyServerSceneChanged(sceneName);
        if (sceneName == "MainGameScene")
            checkAlive = true;
    }

    public override void OnLobbyServerPlayerRemoved(NetworkConnection conn, short playerControllerId)
    {
        base.OnLobbyServerPlayerRemoved(conn, playerControllerId);
        var canvas = FindObjectOfType<Canvas>();
        var button = canvas.GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = this.numPlayers + " Player(s)";
    }

    void Update()
    {
#if SERVER
        if (!checkAlive)
            return;

        if (time < 5f)
        {
            time += Time.deltaTime;
            return;
        }

        int alive = 0;
        bool havePlayers = false;
        // check if all except one player is dead
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pObj in players)
        {
            havePlayers = true;
            if (pObj.transform.position.y >= 1.8f && pObj.transform.position.y < 15.0f)
            {
                ++alive;
            }
        }
        
        if (havePlayers && alive < 2)
        {
            time = 0f;
            checkAlive = false;
            //StopServer();
            ServerReturnToLobby();
        }
#endif
    }

}
