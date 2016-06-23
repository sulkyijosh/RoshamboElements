using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SendScript : MonoBehaviour {


	public void SendToScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
