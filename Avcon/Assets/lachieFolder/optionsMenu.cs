using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XInputDotNetPure;
using System.Collections;

public class optionsMenu : MonoBehaviour{

	public GameObject mainMenu;
	public GameObject option;
	public Button button;
	public Toggle music;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GamePad.GetState (PlayerIndex.One).Buttons.B == ButtonState.Pressed) {
			mainMenu.SetActive (true);
			option.SetActive (true);
			button.Select ();
			this.gameObject.SetActive (false);
		}

		if (music.isOn == false) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}
	}

	public void Music() // this function is for mouse input.
	{
		
	}

	public void Sound()
	{
		
	}

	public void Sensitivity()
	{
		
	}
}
