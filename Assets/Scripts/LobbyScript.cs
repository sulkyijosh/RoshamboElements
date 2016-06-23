using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScript : PunBehaviour {

	public GameObject waitingImage;

	// Use this for initialization
	void Awake ()
	{
		PhotonNetwork.ConnectUsingSettings("1.0");
	}

//	void OnJoinedLobby()
//	{
//		if(PhotonNetwork.room != null)
//		{
//			PhotonNetwork.LeaveRoom();
//		}
//	}

	// Update is called once per frame
	void OnGUI ()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
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
			waitingImage.SetActive(true);
		}
	}

	void OnPhotonPlayerConnected()
	{
		if(PhotonNetwork.playerList.Length == 2)
		{
			waitingImage.SetActive(false);
			PhotonNetwork.LoadLevel("03B_OnlinePlay");
		}
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRandomRoom();
	}
}
