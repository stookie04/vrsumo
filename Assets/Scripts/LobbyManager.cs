using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LobbyManager : NetworkLobbyManager {
    void Start()
    {
        StartCoroutine(CheckNetwork());
    }

    IEnumerator CheckNetwork()
    {
        Debug.Log("IsClientConnected: " + IsClientConnected());
        StartClient();
        yield return new WaitForSeconds(2f);
        Debug.Log("IsClientConnected: " + IsClientConnected());
        if (IsClientConnected())
        {
            Debug.Log("connected as client");
        }
        else
        {
            StopClient();
            yield return new WaitForSeconds(1f);
            StartHost();
            Debug.Log("connected as host");
        }
    }

    public override void OnLobbyClientConnect(NetworkConnection conn)
    {
        //ClientScene.Ready(conn);
        NetworkServer.SetClientNotReady(conn);
        Debug.Log("OnLobbyClientConnect");
        base.OnLobbyClientConnect(conn);
        ClientScene.AddPlayer(conn, 0);
    }
}
