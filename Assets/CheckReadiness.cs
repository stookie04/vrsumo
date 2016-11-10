using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class CheckReadiness : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private LobbyPlayer lobbyPlayer;
    private bool gazing = false;

    // Use this for initialization
    void Start()
    {
        lobbyPlayer = (LobbyPlayer)FindObjectOfType(typeof(LobbyPlayer));
    }

    // Update is called once per frame
    void Update()
    {
        if (!lobbyPlayer)
        {
            //print("still trying to find player");
            lobbyPlayer = (LobbyPlayer)FindObjectOfType(typeof(LobbyPlayer));
        
            if (gazing && lobbyPlayer)
            {
                lobbyPlayer.StartTimer();
                gazing = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!lobbyPlayer)
        {
            gazing = true;
        }
        else
        {
            lobbyPlayer.StartTimer();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gazing = false;
        if (lobbyPlayer)
        {
            lobbyPlayer.StopTimer();
        }
    }
}
