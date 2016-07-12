using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections;
using System;

[System.Serializable]
public class Task {
	public bool active;
	public bool completed;
	public bool held;
	public bool shouldWait;
	public bool waiting;
	public string descOption1;
	public string descOption2;
	public int stress = 0;
	public int startTask;
	public float startDelay;
	public GameObject taskObject;
	public GameObject target;
	public Tasks tasksClass;
	public int tasksActive;
	public string desc = "";
	bool complete;
	public bool activated;
	bool activated2;
	float timeToWait;
	PlayerShooting shootScript;

	public void setMarker () {
		if (taskObject != null && taskObject.transform.FindChild ("questMarker") != null && target != null && target.transform.FindChild ("questMarker") != null) {
			if (active == true && held == false) {
				taskObject.transform.FindChild ("questMarker").gameObject.SetActive (true);
				target.transform.FindChild ("questMarker").gameObject.SetActive (false);
				desc = descOption1;
			} else if (held == true && active == true) {
				target.transform.FindChild ("questMarker").gameObject.SetActive (true);
				taskObject.transform.FindChild ("questMarker").gameObject.SetActive (false);
				desc = descOption2;
			}
		}
	}
		
	public void checkHeld() {
		if (completed == true) {
			active = false;
		}

		if (taskObject != null && taskObject.transform.parent != null) {
			held = taskObject.transform.parent.CompareTag ("Player");
		} else if (taskObject == null || taskObject.transform.parent == null) {
			held = false;
		}

		if (taskObject != null) {
			taskObject.GetComponent<pickUp> ().active = active;
		}

		if (held == true) {
			shootScript.hasObject = true;
		}
	}
	public void checkNextTask() {
		if (active == true && activated == false) {
			activated = true;
			tasksClass.tasksActive++;
			Debug.Log ("Added");
		} else if (active == false && activated == true) {
			activated = false;
			tasksClass.tasksActive--;
			Debug.Log ("removed");
		}

		if (completed == true && complete == false) {
			complete = true;
			desc = "";
			tasksClass.tasksCompleted++;
			active = false;
			GameObject.Destroy (taskObject);
			target.transform.FindChild ("questMarker").gameObject.SetActive (false);
			held = false;
			shootScript.hasObject = false;
		}

		if (taskObject != null && taskObject.GetComponent<pickUp>() != null) {
			completed = taskObject.GetComponent<pickUp> ().completed;
		}
		if (shouldWait == false && waiting == false) {
			waiting = true;
			timeToWait = Time.time + startDelay;
		} else if (waiting == true && Time.time > timeToWait && activated2 == false) {
			activated2 = true;
			active = true;
		}

		if (shouldWait == true && waiting == false && tasksClass.playerTasks[startTask].completed == true){
			shootScript.hasObject = false;
			shouldWait = false;
		}
	}
	public void setValues () {
		if (taskObject != null && target != null) {
			taskObject.GetComponent<pickUp> ().target = target.transform;	
		}
		shootScript = GameObject.Find ("Player").GetComponent<PlayerShooting> ();
	}
}
public class Tasks : MonoBehaviour {
	public Task[] playerTasks;
	public Text taskDesc1;
	public Text taskDesc2;
	public Text taskDesc3;
	public int tasksCompleted = 0;

	public int tasksActive;
	// Use this for initialization
	void Start () {
		foreach (Task element in playerTasks) {
			element.tasksClass = transform.GetComponent<Tasks> ();
			element.setValues ();
		}
	}
	void Awake() { 
	}
	void Update () {
		setText ();
		foreach (Task element in playerTasks) {
			element.setMarker ();
			element.checkHeld ();
			element.checkNextTask ();
		}
	}
	void setText () {
		foreach (Task element in playerTasks) {
			if (playerTasks[0].completed == true)
			taskDesc1.text = "";
			taskDesc2.text = "";
			taskDesc3.text = "";
			int index = Array.IndexOf (playerTasks, element);
			Text textUsed = null;
			if (index != 0) {

				if (element.active == true) {
					switch (tasksActive) {
					case 1:
						taskDesc1.text = element.desc;
						if (textUsed != null)
							textUsed.text = "";
						textUsed = taskDesc1;
						break;
					case 2:
						taskDesc2.text = element.desc;
						if (textUsed != null)
							textUsed.text = "";
						textUsed = taskDesc2;
						break;
					case 3:
						taskDesc3.text = element.desc;
						if (textUsed != null)
							textUsed.text = "";
						textUsed = taskDesc3;
						break;
					default:
						Debug.Log (tasksActive.ToString () + " tasks are active, only three tasks may be active");
						break;
					}
				}
			} else if (index == 0 && element.active == true) {
				taskDesc1.text = element.desc;
			}
		}
	}
	public void printError (string message) {
		Debug.LogError (message);
	}
}