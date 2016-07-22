using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using UnityEngine.EventSystems;
using System.Collections;

public class menu_Options : MonoBehaviour, ISelectHandler, IDeselectHandler {

	public GameObject menu;
	public GameObject optionsMenu;
	public Slider slider;
	public bool selected;

	void Update () 
	{
		if (selected) {
			if (GamePad.GetState (PlayerIndex.One).Buttons.A == ButtonState.Pressed) {
				menu.SetActive (false);
				optionsMenu.SetActive (true);
				slider.Select ();
			}
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log (this.gameObject.name+":"+ "was selected");
		selected = true;
	}

	public void OnDeselect(BaseEventData eventData)
	{
		selected = false;
	}
}
