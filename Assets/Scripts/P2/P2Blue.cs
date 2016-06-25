using UnityEngine;
using System.Collections;

public class P2Blue : CharacterScript {

	bool beaconTouch = false;

	public ParticleSystem particle;
	public Animator animator;
	GameObject home;

	void Start()
	{
		home = GameObject.Find("Pokehome1").gameObject;
	}

	void FixedUpdate ()
	{
		if(!beaconTouch)
		{
			moveCharacter(Vector3.up, animator);
		}
	}

	void OnTriggerEnter(Collider col)
	{

		if(col.tag == "Beacon1")
		{
			attackBase();
			turnTowardsTower(col);
		}
		else
		{
			madeContact(col, particle);
		}
	}

	void attackBase()
	{
		beaconTouch = true;
		attackHome(false, this.gameObject, home, particle);
	}
}
