using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LobbyManager : NetworkLobbyManager {
    
    void Start()
    {
		string[] args = System.Environment.GetCommandLineArgs ();
		if (args.Length > 0) {
			if (args [0] == "-server") {
				StartServer ();
			}
		} else {
			StartClient ();
		}
//        StartCoroutine(CheckNetwork());
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
            Debug.Log("connected as server");
        }
    }
    
    public override void OnLobbyClientConnect(NetworkConnection conn)
    {
        base.OnLobbyClientConnect(conn);
        //ClientScene.Ready(conn);
        NetworkServer.SetClientNotReady(conn);
        Debug.Log("OnLobbyClientConnect");
        //print("player added: " + ClientScene.AddPlayer(conn, 0));
        TryToAddPlayer();
        print("number of players: " + this.numPlayers);
    }
    
}
