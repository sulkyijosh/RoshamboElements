using UnityEngine;
using System.Collections;

public class HomeScript: MonoBehaviour {

	public static int p1HomeHp;
	public static int p2HomeHp;
	public ParticleSystem multiParticle;

	// Use this for initialization
	void Start () {
		p1HomeHp = 3;
		p2HomeHp = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.name == "P1_Pokehome" && p1HomeHp <= 0)
		{
			ParticleSystem thisParticle = Instantiate(multiParticle, this.gameObject.transform.position, Quaternion.identity) as ParticleSystem;
			thisParticle.Play();
			Destroy(this.gameObject);
			endScene();
		}
		else if(this.name == "P2_Pokehome" && p2HomeHp <= 0)
		{
			ParticleSystem thisParticle = Instantiate(multiParticle, this.gameObject.transform.position, Quaternion.identity) as ParticleSystem;
			thisParticle.Play();
			Destroy(this.gameObject);
			endScene();
		}
	}

	void endScene()
	{
		StartCoroutine(GameOver());
	}

	private IEnumerator GameOver()
	{
		yield return new WaitForSeconds(1f);

		if(true)
		{
			
		}
	}
}
