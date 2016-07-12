using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[System.Serializable]
public class Task {
	public bool active;
	public bool held;
	public string desc = "";
	public int stress = 0;
	public GameObject taskObject;
	public GameObject target;



	public void setMarker () {
		if (taskObject != null && taskObject.transform.FindChild ("questMarker") != null && target != null && target.transform.FindChild ("questMarker") != null) {
			if (active == true && held == false) {
				taskObject.transform.FindChild ("questMarker").gameObject.SetActive (true);
				target.transform.FindChild ("questMarker").gameObject.SetActive (false);
			} else if (held == true && active == true) {
				target.transform.FindChild ("questMarker").gameObject.SetActive (true);
				taskObject.transform.FindChild ("questMarker").gameObject.SetActive (false);
			} else {
				taskObject.transform.FindChild ("questMarker").gameObject.SetActive (false);
				target.transform.FindChild ("questMarker").gameObject.SetActive (false);
			}
		}
	}

	public void checkHeld() {
		if (taskObject != null && taskObject.transform.parent != null) {
			held = taskObject.transform.parent.CompareTag ("Player");
		} else {
			held = false;
		}
	}
}

public class Tasks : MonoBehaviour {

	public Task[] playerTasks;

	public Text taskDesc1;
	public Text taskDesc2;
	public Text taskDesc3;

	public int taskNumber = 0;
	// Use this for initialization
	void Start () {
	}

	void Awake() { 
	}
	
	// Update is called once per frame
	void Update () {
		setText ();

		foreach (Task element in playerTasks) {
			element.setMarker ();
			element.checkHeld ();
		}
	}

	void setText () {
		
		if (playerTasks [0] != null) {
			taskDesc1.text = playerTasks [0].desc;
		}

		if (playerTasks.Length > 1) {
			taskDesc2.text = playerTasks [1].desc;
		}

		if (playerTasks.Length > 2) {
			taskDesc3.text = playerTasks [2].desc;
		}
			
	}


}
