using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetManager : NetworkManager
{
    private int playerCount = 0;

    public NetManager()
    {
        maxConnections = 2;
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (conn.address == "localServer")
            return;
        base.OnServerConnect(conn);
        playerCount++;
        Debug.Log(numPlayers);
        float angle = (playerCount-1) * Mathf.PI / 2;
        float height = 8f;
        float scale = 11f;
        Vector3 pos = new Vector3(scale*Mathf.Cos(angle), height, scale*Mathf.Sin(angle));
        GameObject player = (GameObject)Instantiate(playerPrefab, pos, Quaternion.identity);
        NetworkServer.Spawn(player);
    }
}
