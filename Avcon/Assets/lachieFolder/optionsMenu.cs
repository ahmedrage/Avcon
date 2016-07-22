using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XInputDotNetPure;
using System.Collections;

public class optionsMenu : MonoBehaviour{

	public GameObject mainMenu;
	public Button button;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GamePad.GetState (PlayerIndex.One).Buttons.B == ButtonState.Pressed) {
			mainMenu.SetActive (true);
			button.Select ();
			this.gameObject.SetActive (false);
		}
	}
}
