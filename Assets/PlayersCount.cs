using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayersCount : MonoBehaviourPunCallbacks
{

    private byte playerCount = 0;
    public Text counterPlayersText;

    void Start ()
    {
        counterPlayersText = GetComponent<Text>();
        counterPlayersText.text = "Now:" + playerCount.ToString() + " players";
    }

    void Update ()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        counterPlayersText.text = "Now:" + playerCount.ToString() + " players";
    }
}
