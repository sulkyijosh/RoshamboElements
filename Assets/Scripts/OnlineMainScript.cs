using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class OnlineMainScript : PunBehaviour {

	public Camera mainCamera;
	public GameObject p1Interface;
	public GameObject p2Interface;

	public SpriteRenderer p1Beacon1;
	public SpriteRenderer p1Beacon2;
	public SpriteRenderer p1Beacon3;
	public SpriteRenderer p2Beacon1;
	public SpriteRenderer p2Beacon2;
	public SpriteRenderer p2Beacon3;

	private int p1BeaconSelected;
	private int p2BeaconSelected;

	GameObject pokeHome1;
	GameObject pokeHome2;

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

	void Start () {

		if(!PhotonNetwork.isMasterClient)
		{
			p1Interface.SetActive(false);
			p2Interface.SetActive(true);
			isP1 = false;
			mainCamera.transform.position = new Vector3(0f, 0.35f, -10f);
			mainCamera.transform.rotation = Quaternion.Euler(0, 0, 180);
			rotation = new Vector3(0, 0, 180);
		}
		else
		{
			PhotonNetwork.Instantiate("Pokehome1", new Vector3(0f, -3.6f, -3f), Quaternion.Euler(rotation), 0);
			PhotonNetwork.Instantiate("Pokehome2", new Vector3(0f, 3.6f, -3f), Quaternion.Euler(rotation), 0);
			mainCamera.transform.position = new Vector3(0f, -0.35f, -10f);
			isP1 = true;
		}

		StartCoroutine(SpawnAllowed(isP1));

		p1BeaconSelected = 2;
		p2BeaconSelected = 2;

		p1Beacon2.GetComponent<SpriteRenderer>().color = Color.black;
		p2Beacon2.GetComponent<SpriteRenderer>().color = Color.black;
	}

	void Update () {

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
				character = "P1_Fire";
			}
			else if(button == "Green1")
			{
				character = "P1_Earth";
			}
			else
			{
				character = "P1_Water";
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
				character = "P2_Fire";
			}
			else if(button == "Green2")
			{
				character = "P2_Earth";
			}
			else
			{
				character = "P2_Water";
			}

			if(p2BeaconSelected == 1)
			{
				pos = new Vector3(-1.67f, 3.65f, -5f);
			}
			else if(p2BeaconSelected == 2)
			{
				pos = new Vector3(0, 2.6f, -5f);
			}
			else
			{
				pos = new Vector3(1.67f, 3.65f, -5f);
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
			p1Beacon2.GetComponent<SpriteRenderer>().color = Color.white;
			p1Beacon3.GetComponent<SpriteRenderer>().color = Color.white;
			p1Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
		}
		else if (beacon == "p1Beacon2")
		{
			p1BeaconSelected = 2;
			p1Beacon1.GetComponent<SpriteRenderer>().color = Color.white;
			p1Beacon3.GetComponent<SpriteRenderer>().color = Color.white;
			p1Beacon2.GetComponent<SpriteRenderer>().color = Color.black;
		}
		else if (beacon == "p1Beacon3")
		{
			p1BeaconSelected = 3;
			p1Beacon1.GetComponent<SpriteRenderer>().color = Color.white;
			p1Beacon2.GetComponent<SpriteRenderer>().color = Color.white;
			p1Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
		}

		else if (beacon == "p2Beacon1")
		{
			p2BeaconSelected = 1;
			p2Beacon2.GetComponent<SpriteRenderer>().color = Color.white;
			p2Beacon3.GetComponent<SpriteRenderer>().color = Color.white;
			p2Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
		}
		else if (beacon == "p2Beacon2")
		{
			p2BeaconSelected = 2;
			p2Beacon1.GetComponent<SpriteRenderer>().color = Color.white;
			p2Beacon3.GetComponent<SpriteRenderer>().color = Color.white;
			p2Beacon2.GetComponent<SpriteRenderer>().color = Color.black;

		}
		else if (beacon == "p2Beacon3")
		{
			p2BeaconSelected = 3;
			p2Beacon1.GetComponent<SpriteRenderer>().color = Color.white;
			p2Beacon2.GetComponent<SpriteRenderer>().color = Color.white;
			p2Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
		}
	}

	public void EndGame()
	{
		PhotonNetwork.LeaveRoom();
		PhotonNetwork.LoadLevel("03A_Lobby");
	}
}
