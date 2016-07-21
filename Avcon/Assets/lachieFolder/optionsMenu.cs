using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XInputDotNetPure;
using System.Collections;

public class optionsMenu : MonoBehaviour{

	public GameObject mainMenu;
	public Toggle mute;

	// Use this for initialization
	void Start () 
	{
		mute.Select ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GamePad.GetState (PlayerIndex.One).Buttons.B == ButtonState.Pressed) {
			mainMenu.SetActive (true);
			this.gameObject.SetActive (false);
		}
	}
}
