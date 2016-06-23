using UnityEngine;
using System.Collections;
using Photon;

public class OnlineHomeScript : PunBehaviour {

	public static int p1HomeHp;
	public static int p2HomeHp;
	public int startHP = 3;

	public ParticleSystem multiParticle;
	public GameObject winUI;
	public GameObject loseUI;

	private bool pokehome1Dead = false;
	private bool pokehome2Dead = false;

	// Use this for initialization
	void Start () {
//		PhotonNetwork.automaticallySyncScene = true;
		p1HomeHp = startHP;
		p2HomeHp = startHP;
	}
	
	// Update is called once per frame
	void Update () {
		if(PhotonNetwork.isMasterClient){
			
			if(this.name == "Pokehome1(Clone)" && p1HomeHp <= 0 && pokehome1Dead == false)
			{
				pokehome1Dead = true;
				endScene();
//				Debug.Log("Destroyed");
//				GameObject thisParticle = PhotonNetwork.Instantiate("particleMulti", this.gameObject.transform.position, Quaternion.identity, 0);
//				thisParticle.GetComponent<ParticleSystem>().Play();
//				PhotonNetwork.Destroy(this.gameObject);
//				Destroy(thisParticle, 2f);
			}
			else if(this.name == "Pokehome2(Clone)" && p2HomeHp <= 0 && pokehome2Dead == false)
			{
				pokehome2Dead = true;
				endScene();
//				GameObject thisParticle = PhotonNetwork.Instantiate("particleMulti", this.gameObject.transform.position, Quaternion.identity, 0);
//				thisParticle.GetComponent<ParticleSystem>().Play();
//				PhotonNetwork.Destroy(this.gameObject);
//				Destroy(thisParticle, 2f);
			}
		}

		if(pokehome1Dead || pokehome2Dead)
		{
			endScene();
//			PhotonNetwork.LeaveRoom();
//			PhotonNetwork.LoadLevel("03A_Lobby");
		}
	}

	void endScene()
	{
		GameObject thisParticle = PhotonNetwork.Instantiate("particleMulti", this.gameObject.transform.position, Quaternion.identity, 0);
		thisParticle.GetComponent<ParticleSystem>().Play();
		PhotonNetwork.Destroy(this.gameObject);
		Invoke("showEndUI", 2f);
		Destroy(thisParticle, 2f);
		StartCoroutine(GameOver());
	}

	void showEndUI()
	{
		if(PhotonNetwork.isMasterClient)
		{
			winUI.SetActive(true);
		}
		else
		{
			loseUI.SetActive(true);
		}
	}

	private IEnumerator GameOver()
	{
		yield return new WaitForSeconds(3f);

		if(true)
		{
			showEndUI();
		}
	}
}
