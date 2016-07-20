using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using System.Collections;

public class menuScript : MonoBehaviour {
	public GameObject menu;

	bool paused;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && menu.activeSelf == false) {
			Time.timeScale = 0;
			menu.SetActive (true);
		} else if (Input.GetKeyDown (KeyCode.Escape) && menu.activeSelf == true) {
			Resume ();
		}

		if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed && menu.activeSelf == false) {
			Time.timeScale = 0;
			menu.SetActive (true);
		} else if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed && menu.activeSelf == true) {
			Resume ();
		}
			
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	public void Resume () {
		Time.timeScale = 1;
		menu.SetActive (false);
	}

	public void Exit() {
		Application.Quit ();
	}

	public void Restart() {
		Application.LoadLevel (0);
		Time.timeScale = 1;
	}
}