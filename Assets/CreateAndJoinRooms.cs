using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    private byte startPlayerNumber = 5;
    private byte playerCount = 0;
    private bool hasStart = false;

    public void createRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text, new RoomOptions() { MaxPlayers = this.startPlayerNumber}, null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    private void JoinLevel()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if(hasStart){
           PhotonNetwork.LoadLevel("MainScene");
        }
        if(playerCount >= (startPlayerNumber)){
            hasStart = true;
            PhotonNetwork.LoadLevel("MainScene");
        }else{
            PhotonNetwork.LoadLevel("WaiteRoom");
        }
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("teste");
        this.JoinLevel();
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Debug.Log("teste");
        this.JoinLevel();
    }

}
