using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {

	public SpriteRenderer p1Beacon1;
	public SpriteRenderer p1Beacon2;
	public SpriteRenderer p1Beacon3;
	public SpriteRenderer p2Beacon1;
	public SpriteRenderer p2Beacon2;
	public SpriteRenderer p2Beacon3;

	private bool p1SpawnAllowed;
	private bool p2SpawnAllowed;

	private int p1BeaconSelected;
	private int p2BeaconSelected;

	public GameObject p1Red;
	public GameObject p1Green;
	public GameObject p1Blue;
	public GameObject p2Red;
	public GameObject p2Green;
	public GameObject p2Blue;

	public GameObject p1ButtonMask;
	public GameObject p2ButtonMask;

	bool isP1;

	void Start () {
		p1SpawnAllowed = true;
		p2SpawnAllowed = true;

		p1BeaconSelected = 2;
		p2BeaconSelected = 2;

		p1Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
		p2Beacon1.GetComponent<SpriteRenderer>().color = Color.black;
		p1Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
		p2Beacon3.GetComponent<SpriteRenderer>().color = Color.black;
	}

	void Update () {
		
	}

	public void CharacterSelected(string button)
	{
		if (button == "Red1" || button == "Blue1" || button ==  "Green1" && p1SpawnAllowed)
		{
			isP1 = true;
			p1SpawnAllowed = false;
			InstantiateCharacter(true, button);
			p1ButtonMask.SetActive(true);
		}
		else if (button == "Red2" || button == "Blue2" || button == "Green2" && p2SpawnAllowed)
		{
			isP1 = false;
			InstantiateCharacter(false, button);
			p2SpawnAllowed = false;
			p2ButtonMask.SetActive(true);
		}
		StartCoroutine(SpawnAllowed(isP1));
	}

	void InstantiateCharacter(bool isP1, string button)
	{
		GameObject character;
		Vector3 pos;
		Vector3 rotation;

		if(isP1)
		{
			rotation = new Vector3(0, 0, 0);

			if(button == "Red1")
			{
				character = p1Red;
			}
			else if(button == "Green1")
			{
				character = p1Green;
			}
			else
			{
				character = p1Blue;
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
			rotation = new Vector3(0, 0, 180);

			if(button == "Red2")
			{
				character = p2Red;
			}
			else if(button == "Green2")
			{
				character = p2Green;
			}
			else
			{
				character = p2Blue;
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

		Instantiate(character, pos, Quaternion.Euler(rotation));
	}

	void allowSpawn(bool isP1) {
		if (isP1)
		{
			p1ButtonMask.SetActive(false);
			p1SpawnAllowed = true;

		}
		else
		{
			p2ButtonMask.SetActive(false);
			p2SpawnAllowed = true;
		}

	}

	private IEnumerator SpawnAllowed(bool isP1){
		yield return new WaitForSeconds(1f);

		if(true){
			allowSpawn(isP1);
		}
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

	public void endGame()
	{
		SceneManager.LoadScene("01_MainMenu");
	}
}
