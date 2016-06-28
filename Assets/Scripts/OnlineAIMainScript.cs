using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class OnlineAIMainScript : PunBehaviour {

	public Camera mainCamera;

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

	private int p1BeaconSelected;
	private int p2BeaconSelected;
	private int p2CharacterToSpawn;

	GameObject pokeHome1;
	GameObject pokeHome2;

	public GameObject p1ButtonMask;

	private Vector3 rotation = new Vector3(0, 0, 0);

	bool isP1;

	void Awake () {

		p1Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
		p1Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
		pokeHome1 = PhotonNetwork.Instantiate("Pokehome1", new Vector3(0f, -3.6f, -3f), Quaternion.Euler(rotation), 0);
		pokeHome2 = PhotonNetwork.Instantiate("Pokehome2", new Vector3(0f, 3.6f, -3f), Quaternion.Euler(rotation), 0);
		mainCamera.transform.position = new Vector3(0f, -0.35f, -10f);
		isP1 = true;

		StartCoroutine(SpawnAllowed());

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
		isP1 = true;
		p1ButtonMask.SetActive(true);

		InstantiateCharacter(isP1, button);
		StartCoroutine(SpawnAllowed());
	}

	void InstantiateCharacter(bool isP1, string button)
	{
		string character = "Online_P1_Fire";
		Vector3 pos = new Vector3(0, -2.6f, -5f);

		Debug.Log("1");

		if(isP1)
		{
			if(button == "Red1")
			{
				character = "Online_P1_Fire";
				p2CharacterToSpawn = 1;
			}
			else if(button == "Green1")
			{
				character = "Online_P1_Earth";
				p2CharacterToSpawn = 2;
			}
			else
			{
				character = "Online_P1_Water";
				p2CharacterToSpawn = 3;
			}

			if(p1BeaconSelected == 1)
			{
				pos = new Vector3(-1.67f, -3.65f, -5f);
				p2BeaconSelected = 3;
			}
			else if(p1BeaconSelected == 2)
			{
				pos = new Vector3(0, -2.6f, -5f);
				p2BeaconSelected = 2;
			}
			else
			{
				pos = new Vector3(1.67f, -3.65f, -5f);
				p2BeaconSelected = 1;
			}
		}
			

		PhotonNetwork.Instantiate(character, pos, Quaternion.Euler(rotation), 0);
		StartCoroutine(SpawnAICharacter());
	}

	void IAInstantiateCharacter(int beacon, int character)
	{
		Vector3 pos;
		int spawnPicker = Random.Range(1, 100);
		string characterToSpawn;
		Debug.Log("spawnpicker = " + spawnPicker);

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

		if(character == 1)
		{
			if(spawnPicker <= 10)
			{
				characterToSpawn = "Online_P2_Earth";
			}
			else if(spawnPicker >= 11 && spawnPicker <= 50)
			{
				characterToSpawn = "Online_P2_Fire";
			}
			else
			{
				characterToSpawn = "Online_P2_Water";
			}
		}
		else if(character == 2)
		{
			if(spawnPicker <= 10)
			{
				characterToSpawn = "Online_P2_Water";
			}
			else if(spawnPicker >= 11 && spawnPicker <= 50)
			{
				characterToSpawn = "Online_P2_Earth";
			}
			else
			{
				characterToSpawn = "Online_P2_Fire";
			}
		}
		else
		{
			if(spawnPicker <= 10)
			{
				characterToSpawn = "Online_P2_Fire";
			}
			else if(spawnPicker >= 11 && spawnPicker <= 50)
			{
				characterToSpawn = "Online_P2_Water";
			}
			else
			{
				characterToSpawn = "Online_P2_Earth";
			}
		}
		PhotonNetwork.Instantiate(characterToSpawn, pos, Quaternion.Euler(0, 0, 180), 0);
	}

	void allowSpawn() {
		p1ButtonMask.SetActive(false);
	}

	private IEnumerator SpawnAllowed()
	{
		yield return new WaitForSeconds(1f);

		allowSpawn();
	}

	private IEnumerator SpawnAICharacter()
	{
		float secondsToWait = Random.Range(0.4f, 0.9f);
		yield return new WaitForSeconds(secondsToWait);

		IAInstantiateCharacter(p2BeaconSelected, p2CharacterToSpawn);
		Debug.Log(p2BeaconSelected);
		Debug.Log(p2CharacterToSpawn);
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
