using UnityEngine;
using System.Collections;

public class OnlineCharacterScript : MonoBehaviour {

	private float movementSpeed = 1.5f;
	public ParticleSystem particleRed;
	public ParticleSystem particleGreen;
	public ParticleSystem particleBlue;
	ParticleSystem thisParticle;

	public void moveCharacter (Vector3 directionToMove, Animator animator) {
		this.gameObject.transform.Translate(directionToMove * Time.deltaTime * movementSpeed);
		animator.Play("Walk", 0);
	}

	public void madeContact(Collider opponent, ParticleSystem particle) 
	{

		Debug.Log(opponent.gameObject.name);
		switch (opponent.gameObject.tag)
		{
		case "Red1":
			if(this.gameObject.tag == "Blue2" || this.gameObject.tag == "Red2")
			{
				killOpponentCharacter(particleRed, opponent);
			}

			if(this.gameObject.tag == "Green2" || this.gameObject.tag == "Red2")
			{
				killThisCharacter(particle, opponent);
			}
			break;

		case "Green1":
			if(this.gameObject.tag == "Green2" || this.gameObject.tag == "Red2")
			{
				killOpponentCharacter(particleGreen, opponent);
			}
			if(this.gameObject.tag == "Green2" || this.gameObject.tag == "Blue2")
			{
				killThisCharacter(particle, opponent);
			}
			break;

		case "Blue1":
			if(this.gameObject.tag == "Green2" || this.gameObject.tag == "Blue2")
			{
				killOpponentCharacter(particleBlue, opponent);
			}
			if(this.gameObject.tag == "Red2" || this.gameObject.tag == "Blue2")
			{
				killThisCharacter(particle, opponent);
			}
			break;
		case "Red2":
			if(this.gameObject.tag == "Blue1" || this.gameObject.tag == "Red1")
			{
				killOpponentCharacter(particleRed, opponent);
			}
			if(this.gameObject.tag == "Green1" || this.gameObject.tag == "Red1")
			{
				killThisCharacter(particle, opponent);
			}
			break;

		case "Green2":
			if(this.gameObject.tag == "Green1" || this.gameObject.tag == "Red1")
			{
				killOpponentCharacter(particleGreen, opponent);
			}
			if(this.gameObject.tag == "Green1" || this.gameObject.tag == "Blue1")
			{
				killThisCharacter(particle, opponent);
			}
			break;

		case "Blue2":
			if(this.gameObject.tag == "Green1" || this.gameObject.tag == "Blue1")
			{
				killOpponentCharacter(particleBlue, opponent);
			}
			if(this.gameObject.tag == "Red1" || this.gameObject.tag == "Blue1")
			{
				killThisCharacter(particle, opponent);
			}
			break;

		default:
			break;
		}
	}

	void killThisCharacter(ParticleSystem particle, Collider opponent)
	{
		attackEffect(particle, opponent.gameObject);
		Destroy(this.gameObject);
	}
	void killOpponentCharacter(ParticleSystem particle, Collider opponent)
	{
		attackEffect(particle, this.gameObject);
		Destroy(opponent.gameObject);
	}


	public void attackHome(bool isP1, GameObject character, GameObject home, ParticleSystem particle){

		if(character != null && home != null)
		{
			if (isP1)
			{
				OnlineHomeScript.p2HomeHp -= 1;
			}
			else
			{
				OnlineHomeScript.p1HomeHp -= 1;
			}
			attackEffect(particle, home);
			StartCoroutine(prepareAttack(isP1, character, home, particle));
		}
	}

	void attackEffect(ParticleSystem particle, GameObject location)
	{
		ParticleSystem thisParticle = Instantiate(particle, location.transform.position, Quaternion.identity) as ParticleSystem;
		thisParticle.Play();
		Destroy(thisParticle.gameObject, 1f);
	}

	private IEnumerator prepareAttack(bool isP1, GameObject character, GameObject home, ParticleSystem particle){
		yield return new WaitForSeconds(1f);

//		if(true){
			attackHome(isP1, character, home, particle);
//		}
	}
}
