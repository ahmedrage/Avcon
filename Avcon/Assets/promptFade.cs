using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class promptFade : MonoBehaviour {

	public Image screeen;
	public Image controller;
	public Text text;
	public GameObject warning;
	public float speed;


	// Use this for initialization
	void Start () 
	{
		StartCoroutine ("fadeDelay");
	}
	
	IEnumerator fadeDelay()
	{
		yield return new WaitForSeconds (3);
		StartCoroutine (Fade (Color.black, Color.clear, 1));
	}

	IEnumerator Fade(Color A, Color B, float time)
	{
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) 
		{
			percent += Time.deltaTime * speed;
			screeen.color = Color.Lerp (A, B, percent);
			controller.color = Color.Lerp (Color.white, Color.clear, percent);
			text.color = Color.Lerp (Color.white, Color.clear, percent);
			yield return null;
		}

		if (percent >= 1) {
			Destroy (warning);
		}
	}
}
