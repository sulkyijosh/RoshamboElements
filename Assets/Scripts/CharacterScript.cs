using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	private float movementSpeed = 1.5f;

	public void moveCharacter (Vector3 directionToMove, Animator animator) {
		this.gameObject.transform.Translate(directionToMove * Time.deltaTime * movementSpeed);
		animator.Play("Walk", 0);
	}

	public void madeContact(Collider opponent, ParticleSystem particle) 
	{

		switch (opponent.gameObject.tag)
		{
		case "Red1":
			if(this.gameObject.tag == "Green2" || this.gameObject.tag == "Red2")
			{
				killCharacter(particle, opponent);
			}
			break;

		case "Green1":
			if(this.gameObject.tag == "Green2" || this.gameObject.tag == "Blue2")
			{
				killCharacter(particle, opponent);
			}
			break;

		case "Blue1":
			if(this.gameObject.tag == "Red2" || this.gameObject.tag == "Blue2")
			{
				killCharacter(particle, opponent);
			}
			break;
		case "Red2":
			if(this.gameObject.tag == "Green1" || this.gameObject.tag == "Red1")
			{
				killCharacter(particle, opponent);
			}
			break;

		case "Green2":
			if(this.gameObject.tag == "Green1" || this.gameObject.tag == "Blue1")
			{
				killCharacter(particle, opponent);
			}
			break;

		case "Blue2":
			if(this.gameObject.tag == "Red1" || this.gameObject.tag == "Blue1")
			{
				killCharacter(particle, opponent);
			}
			break;

		default:
			break;
		}
	}

	void killCharacter(ParticleSystem particle, Collider opponent)
	{
		attackEffect(particle, opponent.gameObject);
		Destroy(this.gameObject);
	}

	public void attackHome(bool isP1, GameObject character, GameObject home, ParticleSystem particle){

		if(character != null && home != null)
		{
			if (isP1)
			{
				HomeScript.p2HomeHp -= 1;
			}
			else
			{
				HomeScript.p1HomeHp -= 1;
			}
			Debug.Log(HomeScript.p1HomeHp);
			Debug.Log(HomeScript.p2HomeHp);
			attackEffect(particle, home);
			StartCoroutine(prepareAttack(isP1, character, home, particle));
		}
	}

	void attackEffect(ParticleSystem particle, GameObject home)
	{
		ParticleSystem thisParticle = Instantiate(particle, home.transform.position, Quaternion.identity) as ParticleSystem;
		thisParticle.Play();
		Destroy(thisParticle.gameObject, 1f);
	}

	public void turnTowardsTower(Collider col)
	{
		if(col.gameObject.name == "Beacon1")
		{
			this.gameObject.transform.Rotate(new Vector3(0, 0, 90));
		}
		else if(col.gameObject.name == "Beacon3")
		{
			this.gameObject.transform.Rotate(new Vector3(0, 0, -90));
		}
	}

	private IEnumerator prepareAttack(bool isP1, GameObject character, GameObject home, ParticleSystem particle){
		yield return new WaitForSeconds(1f);

		if(true){
			attackHome(isP1, character, home, particle);
		}
	}
}
