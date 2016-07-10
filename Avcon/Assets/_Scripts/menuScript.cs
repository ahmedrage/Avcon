using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuScript : MonoBehaviour {
	public GameObject menu;
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
	}

	public void Resume () {
		Time.timeScale = 1;
		menu.SetActive (false);
	}

	public void Exit() {
		Application.Quit ();
	}
}