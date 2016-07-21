using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public Text timerText;
	private float startTime;
	public float timer;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.time - startTime; 

		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString("f0");

		timerText.text = "Time" + minutes + ":" + seconds;
		//timer = minutes + seconds;
	}
}
