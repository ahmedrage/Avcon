using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public Text timerText;
	private float startTime;
	public float endTime;

	[HideInInspector] bool completed;
	// Use this for initialization
	void Start () {
		startTime = Time.time - endTime;
	}
	
	// Update is called once per frame
	void Update () {
		float t = -Time.time - startTime; 

		if (t > 0) {
			string minutes = ((int)t / 60).ToString ();
			string seconds = (t % 60).ToString ("f0");

			timerText.text = "Time " + minutes + ":" + seconds;
		} else if (completed == false) {
			timerText.text = "Time's up";
			completed = true;
		}
	}
}
