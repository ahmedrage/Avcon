using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	public void Play(){
		StartCoroutine ("playDelay");
	}

	public void Quit(){
		Application.Quit ();
	}

	IEnumerator playDelay(){
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene ("OneStart");
	}
}
