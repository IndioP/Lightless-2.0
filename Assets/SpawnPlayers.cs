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
    int idMonster = 5;


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
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-1,1),UnityEngine.Random.Range(-1,1));
            PhotonNetwork.Instantiate(playerPrefab[0].name, pos, Quaternion.identity,0);
        }
        else{
            Debug.Log("Player");
            int numberPrefab = UnityEngine.Random.Range(1, numberPlayers);
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-1,1),UnityEngine.Random.Range(-1,1));
            PhotonNetwork.Instantiate(playerPrefab[numberPrefab].name, pos, Quaternion.identity,0);
        }
        
    }
}
