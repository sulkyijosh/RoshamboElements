using UnityEngine;
using System.Collections;

public class P1Blue : OnlineCharacterScript {
	
	bool beaconTouch = false;

	public ParticleSystem particle;
	public Animator animator;
	GameObject home;

	void Start()
	{
		home = GameObject.Find("Pokehome2(Clone)").gameObject;
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
		if(col.tag == "Beacon2")
		{
			attackBase();
		}
		else
		{
			madeContact(col, particle);
		}
	}

	void attackBase()
	{
		beaconTouch = true;
		attackHome(true, this.gameObject, home, particle);
	}
}
