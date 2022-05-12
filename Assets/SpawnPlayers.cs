using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;
using System;

public class SpawnPlayers : MonoBehaviour{
    public List<GameObject> playerPrefab;
    int numberPlayers = 0; 
    int idMonster = 3;


    private void Start(){
        numberPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        //int idMonster = Math.Ceiling(numberPlayers/2);

        for(int i = 0; i < numberPlayers; i++){
            Debug.Log("ID: " + PhotonNetwork.PlayerList[i]);
        }
        
        Debug.Log("ID PLAYER LOCAL: " + PhotonNetwork.LocalPlayer.ActorNumber);
        Debug.Log("ID MONSTER: " + idMonster);
        if(PhotonNetwork.LocalPlayer.ActorNumber == idMonster){
            Debug.Log("MonsterID: " + PhotonNetwork.LocalPlayer.ActorNumber);
            Vector2 pos = new Vector2(1,1);
            PhotonNetwork.Instantiate(playerPrefab[0].name, pos, Quaternion.identity,0);
        }
        else{
            Debug.Log("Player");
            int numberPrefab = UnityEngine.Random.Range(1, numberPlayers);
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-3,3),UnityEngine.Random.Range(-3,3));
            PhotonNetwork.Instantiate(playerPrefab[numberPrefab].name, pos, Quaternion.identity,0);
        }
        
    }
    private void Update()
    {
        Boolean gameHasEnded = true;
        int numPlayer = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Jogador");
        foreach (var player in players)
        {
            numPlayer += 1;
            SpriteRenderer[] sprites = player.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                if(sprite.tag == "SpriteCaveira")
                {
                    if (!sprite.enabled)
                    {
                        gameHasEnded = false;
                    }
                }
            }
        }
        if (gameHasEnded && numPlayer > 0)
        {
            PhotonNetwork.LoadLevel("GameOver");
        }
    }

}
