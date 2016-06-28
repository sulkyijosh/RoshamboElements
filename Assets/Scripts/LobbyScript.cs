using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class LobbyScript : PunBehaviour {

	public GameObject waitingScreen;
	public GameObject loadingScreen;

	bool opponentNotFound = true;

	Thread AIMatching;// = new Thread(AIMatchingRef);

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
			opponentNotFound = true;
			StartCoroutine(MatchAIOpponent());
		}
	}

	void OnPhotonPlayerConnected()
	{
		if(PhotonNetwork.playerList.Length == 2)
		{
			stopAIMatch();
			PhotonNetwork.LoadLevel("03B_OnlinePlay");
		}
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRandomRoom();
		waitingScreen.SetActive(true);
	}

	public void cancelWait()
	{
		waitingScreen.SetActive(false);
		stopAIMatch();
		PhotonNetwork.LeaveRoom();
	}

	private IEnumerator MatchAIOpponent()
	{
		yield return new WaitForSeconds(10f);

		if(opponentNotFound)
		{
			PhotonNetwork.LoadLevel("03C_AIPlay");
		}
	}

	public void stopAIMatch()
	{
		opponentNotFound = false;
		StopCoroutine(MatchAIOpponent());
	}

//	public static void MatchAIOpponent()
//	{
//		ThreadStart AIMatchingRef = new ThreadStart(MatchAIOpponent);
//		AIMatching = new Thread(AIMatchingRef);
//		int waitTime = 3000;
//
//		Thread.Sleep(waitTime);
//		
//	}
//
//	void AIMatch()
//	{
//		PhotonNetwork.LoadLevel("02_Offline");
//	}
}
