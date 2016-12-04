#define CLIENT

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LobbyManager : NetworkLobbyManager {
    
    bool checkAlive = false;
    float time = 0f;

    void Awake()
    {
        print("awake");
        checkAlive = false;
        time = 0f;
#if SERVER
        StartServer();
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

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        print("add player: " + conn.connectionId);
        var canvas = FindObjectOfType<Canvas>();
        var button = canvas.GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = this.numPlayers + " Player(s)";
    }

    public override void OnLobbyServerSceneChanged(string sceneName)
    {
        base.OnLobbyServerSceneChanged(sceneName);
        checkAlive = true;
    }

    void Update()
    {
#if SERVER
        if (!checkAlive && time < 2)
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
            print(pObj.transform.position.y);
            if (pObj.transform.position.y >= 1.8f && pObj.transform.position.y < 15.0f)
            {
                ++alive;
            }
        }
        
        if (havePlayers && alive < 2)
        {
            print("reset game");
            StopServer();
            ServerReturnToLobby();
        }
#endif
    }

}
