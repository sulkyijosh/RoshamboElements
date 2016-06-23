using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 1);
	}
}
