using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Timer : MonoBehaviour {
	public Text timerText;
	public float endTime;
	public textState[] textStates;
	Tasks taskScript;
	float startTime;
	public float timeLeft;
	[HideInInspector] bool completed;
	// Use this for initialization
	void Start () {
		startTime = Time.time - endTime;
		taskScript = GameObject.Find ("GM").GetComponent<Tasks> ();
		InvokeRepeating ("setColor", 0.1f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		setTime ();
	}

	void setTime () {
		if (timeLeft >= 0) {
			timeLeft = -Time.time - startTime; 
		}
		if (timeLeft > 0) {
			string minutes = ((int)timeLeft / 60).ToString ();
			string seconds = (timeLeft % 60).ToString ("f0");

			timerText.text = "Time " + minutes + ":" + seconds;
		} else if (completed == false) {
			taskScript.finishTasks ();
			timerText.text = "Time's up";
			timeLeft = 0;
			completed = true;
		}
	}

	void setColor() {
		foreach (textState element in textStates) {
			int index = Array.IndexOf (textStates, element);
			if (index != 0 && textStates [index - 1].Completed == true && element.Completed == false && timeLeft <= element.stateTimeStart) {
				timerText.color = element.stateColor;
				element.Completed = true;
			} else if (index == 0 && element.Completed == false) {
				timerText.color = element.stateColor;
				element.Completed = true;
			}
		}
	}
}
[System.Serializable]
public class textState {
	public Color stateColor;
	public float stateTimeStart;
	public bool Completed;
}
