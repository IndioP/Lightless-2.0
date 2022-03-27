using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;


    private void Start()
    {
        Vector2 pos = new Vector2(0, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
    }
}
