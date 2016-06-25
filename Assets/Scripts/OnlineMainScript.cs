using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class OnlineMainScript : PunBehaviour {

	public Camera mainCamera;
	public GameObject p1Interface;
	public GameObject p2Interface;

	private bool pokehome1Dead = false;
	private bool pokehome2Dead = false;

	public static int p1HomeHp;
	public static int p2HomeHp;
	public int startHP = 3;

	public GameObject winUI;
	public GameObject loseUI;

	public SpriteRenderer p1Beacon1;
	public SpriteRenderer p1Beacon2;
	public SpriteRenderer p1Beacon3;
	public SpriteRenderer p2Beacon1;
	public SpriteRenderer p2Beacon2;
	public SpriteRenderer p2Beacon3;

	private int p1BeaconSelected;
	private int p2BeaconSelected;

	public GameObject pokeHome1;
	public GameObject pokeHome2;

	public GameObject p1Red;
	public GameObject p1Green;
	public GameObject p1Blue;
	public GameObject p2Red;
	public GameObject p2Green;
	public GameObject p2Blue;

	public GameObject p1ButtonMask;
	public GameObject p2ButtonMask;

	private Vector3 rotation = new Vector3(0, 0, 0);

	bool isP1;

	void Awake () {

		if(!PhotonNetwork.isMasterClient)
		{
			p1Interface.SetActive(false);
			p2Interface.SetActive(true);
			isP1 = false;
			mainCamera.transform.position = new Vector3(0f, 0.35f, -10f);
			mainCamera.transform.rotation = Quaternion.Euler(0, 0, 180);
			rotation = new Vector3(0, 0, 180);
			p2Beacon2.GetComponent<SpriteRenderer>().color = Color.white;
		}
		else
		{
			p1Beacon2.GetComponent<SpriteRenderer>().color = Color.white;
			pokeHome1 = PhotonNetwork.Instantiate("Pokehome1", new Vector3(0f, -3.6f, -3f), Quaternion.Euler(rotation), 0);
			pokeHome2 = PhotonNetwork.Instantiate("Pokehome2", new Vector3(0f, 3.6f, -3f), Quaternion.Euler(rotation), 0);
			mainCamera.transform.position = new Vector3(0f, -0.35f, -10f);
			isP1 = true;
		}

		StartCoroutine(SpawnAllowed(isP1));

		p1BeaconSelected = 2;
		p2BeaconSelected = 2;

		p1HomeHp = startHP;
		p2HomeHp = startHP;
	}

	void Update () {
		
		if(p1HomeHp <= 0 && pokehome1Dead == false && pokehome2Dead == false)
		{
			pokehome1Dead = true;
			showEndUI();
		}
		else if(p2HomeHp <= 0 && pokehome2Dead == false && pokehome1Dead == false)
		{
			pokehome2Dead = true;
			showEndUI();
		}
	}

	public void CharacterSelected(string button)
	{
		if (button == "Red1" || button == "Blue1" || button ==  "Green1")
		{
			isP1 = true;
			p1ButtonMask.SetActive(true);
		}
		else if (button == "Red2" || button == "Blue2" || button == "Green2")
		{
			
			isP1 = false;
			p2ButtonMask.SetActive(true);
		}
		InstantiateCharacter(isP1, button);
		StartCoroutine(SpawnAllowed(isP1));
	}

	void InstantiateCharacter(bool isP1, string button)
	{
		string character;
		Vector3 pos;

		if(isP1)
		{
			if(button == "Red1")
			{
				character = "Online_P1_Fire";
			}
			else if(button == "Green1")
			{
				character = "Online_P1_Earth";
			}
			else
			{
				character = "Online_P1_Water";
			}

			if(p1BeaconSelected == 1)
			{
				pos = new Vector3(-1.67f, -3.65f, -5f);
			}
			else if(p1BeaconSelected == 2)
			{
				pos = new Vector3(0, -2.6f, -5f);
			}
			else
			{
				pos = new Vector3(1.67f, -3.65f, -5f);
			}
			
		}
		else
		{
			if(button == "Red2")
			{
				character = "Online_P2_Fire";
			}
			else if(button == "Green2")
			{
				character = "Online_P2_Earth";
			}
			else
			{
				character = "Online_P2_Water";
			}

			if(p2BeaconSelected == 1)
			{
				pos = new Vector3(1.67f, 3.65f, -5f);
			}
			else if(p2BeaconSelected == 2)
			{
				pos = new Vector3(0, 2.6f, -5f);
			}
			else
			{
				pos = new Vector3(-1.67f, 3.65f, -5f);
			}
		}

		PhotonNetwork.Instantiate(character, pos, Quaternion.Euler(rotation), 0);
	}

	void allowSpawn(bool isP1) {
		if (isP1)
		{
			p1ButtonMask.SetActive(false);
		}
		else
		{
			p2ButtonMask.SetActive(false);
		}

	}

	private IEnumerator SpawnAllowed(bool isP1){
		yield return new WaitForSeconds(1f);

		allowSpawn(isP1);
	}

	public void ButtonSelected(string beacon)
	{
		if (beacon == "p1Beacon1")
		{
			p1BeaconSelected = 1;
			p1Beacon2.GetComponent<SpriteRenderer>().color = Color.black;
			p1Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
			p1Beacon1.GetComponent<SpriteRenderer>().color = Color.white;
		}
		else if (beacon == "p1Beacon2")
		{
			p1BeaconSelected = 2;
			p1Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
			p1Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
			p1Beacon2.GetComponent<SpriteRenderer>().color = Color.white;
		}
		else if (beacon == "p1Beacon3")
		{
			p1BeaconSelected = 3;
			p1Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
			p1Beacon2.GetComponent<SpriteRenderer>().color = Color.black;
			p1Beacon3.GetComponent<SpriteRenderer>().color = Color.white;
		}

		else if (beacon == "p2Beacon1")
		{
			p2BeaconSelected = 1;
			p2Beacon2.GetComponent<SpriteRenderer>().color = Color.black;
			p2Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
			p2Beacon1.GetComponent<SpriteRenderer>().color = Color.white;
		}
		else if (beacon == "p2Beacon2")
		{
			p2BeaconSelected = 2;
			p2Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
			p2Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
			p2Beacon2.GetComponent<SpriteRenderer>().color = Color.white;

		}
		else if (beacon == "p2Beacon3")
		{
			p2BeaconSelected = 3;
			p2Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
			p2Beacon2.GetComponent<SpriteRenderer>().color = Color.black;
			p2Beacon3.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	public void EndGame()
	{
		PhotonNetwork.LeaveRoom();
		PhotonNetwork.LoadLevel("03A_Lobby");
	}

	void endScene(GameObject pokeHome)
	{
		GameObject thisParticle = PhotonNetwork.Instantiate("particleMulti", pokeHome.transform.position, Quaternion.identity, 0);
		thisParticle.GetComponent<ParticleSystem>().Play();
		Destroy(thisParticle, 2f);
		PhotonNetwork.Destroy(pokeHome);
	}

	void showEndUI()
	{
		if(PhotonNetwork.isMasterClient)
		{
			if(pokehome2Dead)
			{
				winUI.SetActive(true);	
				endScene(pokeHome2);
			}
			else
			{
				loseUI.SetActive(true);
				endScene(pokeHome1);
			}
		}
		else
		{
			if(pokehome2Dead)
			{
				loseUI.SetActive(true);
				endScene(pokeHome1);
			}
			else
			{
				winUI.SetActive(true);
				endScene(pokeHome2);
			}
		}
	}

	private IEnumerator GameOver()
	{
		yield return new WaitForSeconds(2f);

		if(true)
		{
			showEndUI();
		}
	}
}
