using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class menu_Play : MonoBehaviour,ISelectHandler,IDeselectHandler {

	public GameObject pressToStart;
	public GameObject Ximg;
	public Image xButton;
	public Text prompt;
	public float duration; 

	private bool selected;

	void Start()
	{
	}

	void Update () 
	{
		if (selected) {
			if (GamePad.GetState (PlayerIndex.One).IsConnected == true) {
				pressToStart.SetActive (true); 
				Ximg.SetActive (true);
			} else {
				pressToStart.SetActive (false);
				Ximg.SetActive (false);
			}

			if (GamePad.GetState (PlayerIndex.One).Buttons.X == ButtonState.Pressed) {
				SceneManager.LoadScene ("OneStart");
			}

			prompt.color = Color.Lerp (Color.clear, Color.white, Mathf.PingPong (Time.time, duration));
			xButton.color = Color.Lerp (Color.clear, Color.white, Mathf.PingPong (Time.time, duration));
		} else {
			pressToStart.SetActive (false);
			Ximg.SetActive (false);
		}
	}

	public void Play()
	{
		SceneManager.LoadScene ("OneStart");
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
