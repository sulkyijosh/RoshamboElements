using UnityEngine;
using System.Collections;
using Photon;

public class GameNetworkScript : PunBehaviour {


	public void endGame()
	{
		PhotonNetwork.LoadLevel("03A_Lobby");
	}
}
