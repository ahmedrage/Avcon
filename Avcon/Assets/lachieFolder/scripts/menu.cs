using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	public GameObject pressToStart;
	public Text prompt;
	public Image xButton;
	public GameObject Ximg;
	public GameObject playImg;
	public GameObject quitImg;
	public float duration;

	public void Update()
	{

		if (GamePad.GetState (PlayerIndex.One).IsConnected == true) {
			pressToStart.SetActive (true);
			Ximg.SetActive (true);
			playImg.SetActive (false);
			quitImg.SetActive (false);
		} else {
			pressToStart.SetActive (false);
			Ximg.SetActive (false);
			playImg.SetActive (true);
			quitImg.SetActive (true);
		}

		if (GamePad.GetState (PlayerIndex.One).Buttons.X == ButtonState.Pressed) {
			SceneManager.LoadScene ("OneStart");
		}

		prompt.color = Color.Lerp (Color.clear, Color.white, Mathf.PingPong (Time.time, duration));
		xButton.color = Color.Lerp (Color.clear, Color.white, Mathf.PingPong (Time.time, duration));
	}

	public void Play(){
		StartCoroutine ("playDelay");
	}

	public void Quit(){
		Application.Quit ();
	}
}
