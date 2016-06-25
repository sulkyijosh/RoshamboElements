using UnityEngine;
using System.Collections;

public class HomeScript: MonoBehaviour {

	public static int p1HomeHp;
	public static int p2HomeHp;
	public ParticleSystem multiParticle;
	public GameObject endNotice;
	public GameObject home1;
	public GameObject home2;

	void Start () {
		p1HomeHp = 3;
		p2HomeHp = 3;
	}

	void Update () {
		if(home1 != null && home2 != null)
		{
			if(p1HomeHp <= 0)
			{
				endScene(home1, true);
			}
			else if(p2HomeHp <= 0)
			{
				endScene(home2, false);
			}
		}
	}

	void endScene(GameObject home, bool p1Wins)
	{
		ParticleSystem thisParticle = Instantiate(multiParticle, home.transform.position, Quaternion.identity) as ParticleSystem;
		thisParticle.Play();
		Destroy(home.gameObject);

		if(!p1Wins)
		{
			endNotice.transform.Rotate(new Vector3(0, 0, 180));
		}
		endNotice.SetActive(true);
	}
}
