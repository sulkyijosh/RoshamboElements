using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScript : PunBehaviour {

	public GameObject waitingScreen;
	public GameObject loadingScreen;

	// Use this for initialization
	void Awake ()
	{
		if(PhotonNetwork.connectionState == ConnectionState.Disconnected)
		{
			PhotonNetwork.ConnectUsingSettings("1.0");
		}
	}

	public override void OnJoinedLobby()
	{
		loadingScreen.SetActive(false);
	}

	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}

	public override void OnJoinedRoom()
	{
		
		if(PhotonNetwork.playerList.Length == 2)
		{
			PhotonNetwork.LoadLevel("03B_OnlinePlay");
		}
		else
		{
			waitingScreen.SetActive(true);
		}
	}

	void OnPhotonPlayerConnected()
	{
		if(PhotonNetwork.playerList.Length == 2)
		{
			waitingScreen.SetActive(false);
			PhotonNetwork.LoadLevel("03B_OnlinePlay");
		}
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	public void cancelWait()
	{
		waitingScreen.SetActive(false);
		PhotonNetwork.LeaveRoom();
	}
}
