using UnityEngine;
using System.Collections;

public class P2Red : OnlineCharacterScript {

	bool beaconTouch = false;

	public ParticleSystem particle;
	public Animator animator;
	GameObject home;

	void Start()
	{
		home = GameObject.Find("Pokehome1(Clone)").gameObject;
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
		}
		else
		{
			
		}
	}

	void attackBase()
	{
		beaconTouch = true;
		attackHome(false, this.gameObject, home, particle);
	}
}
