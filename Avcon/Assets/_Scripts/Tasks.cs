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
	public bool displayed;



	PlayerShooting shootScript;

	public void setMarker () {
		if (taskObject != null && taskObject.transform.FindChild ("questMarker") != null && target != null && target.transform.FindChild ("questMarker") != null) {
			if (active == true && held == false && desc != descOption1) {
				taskObject.transform.FindChild ("questMarker").gameObject.SetActive (true);
				target.transform.FindChild ("questMarker").gameObject.SetActive (false);
				desc = descOption1;
				tasksClass.setText ();
			} else if (held == true && active == true && desc != descOption2) {
				target.transform.FindChild ("questMarker").gameObject.SetActive (true);
				taskObject.transform.FindChild ("questMarker").gameObject.SetActive (false);
				desc = descOption2;
				tasksClass.setText ();
			}
		}
	}
		
	public void checkHeld() {
		if (completed == true) {
			active = false;
		}

		if (taskObject != null && taskObject.transform.parent != null) {
			held = taskObject.transform.parent.CompareTag ("Hands");
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
			tasksClass.audioStart.Play ();
			activated = true;
			tasksClass.tasksActive++;
			Debug.Log ("Added");
			tasksClass.setText ();
		} else if (active == false && activated == true) {
			activated = false;
			tasksClass.tasksActive--;
			Debug.Log ("removed");
		}

		if (completed == true && complete == false) {
			complete = true;
			desc = "";
			tasksClass.tasksCompleted++;
			//tasksClass.tasksDisplayed--;
			active = false;
			GameObject.Destroy (taskObject);
			target.transform.FindChild ("questMarker").gameObject.SetActive (false);
			held = false;
			shootScript.hasObject = false;
			tasksClass.setText ();
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
	public AudioSource audioStart;
	public int tasksDisplayed;
	public Light Scenelight;
	public UnityStandardAssets.ImageEffects.MotionBlur motionBlur;
	public float blurAmmount;
	bool ending;
	bool ended;
	// Use this for initialization
	void Awake() { 
	}
	void Update () {
		if (ending == true && ended == false) {
			ended = true;
			motionBlur.blurAmount = blurAmmount;
		}

		setText ();
		foreach (Task element in playerTasks) {
			element.checkHeld ();
			element.checkNextTask ();
			element.setMarker ();
		}
	}

	void taskUpdate() {

		
	}

	void Start () {
		motionBlur = GameObject.Find("FirstPersonCharacter").GetComponent<UnityStandardAssets.ImageEffects.MotionBlur> ();
		foreach (Task element in playerTasks) {
			element.tasksClass = transform.GetComponent<Tasks> ();
			element.setValues ();
		}
		InvokeRepeating ("taskUpdate", 0, 0.1f);

	}
	public void setText () {
		
		//print ("Case2");
		foreach (Task element in playerTasks) {
			
			//if (playerTasks[0].completed == true)
			//taskDesc1.text = "";
			//taskDesc2.text = "";
			//taskDesc3.text = "";
			int index = Array.IndexOf (playerTasks, element);

			if (index != 0) {

				if (element.active == true) {
					if (element.displayed == false) {
						element.displayed = true;
					}
					switch (tasksActive) {
					case 1:
						print ("Case1");
						taskDesc1.text = element.desc;
						taskDesc2.text = "";
						taskDesc3.text = "";

						break;
					case 2:
						print ("Case2");
						taskDesc3.text = "";
						if (tasksDisplayed == 0) {
							taskDesc1.text = element.desc;
						} else if (tasksDisplayed == 1) {
							taskDesc2.text = element.desc;
						} else {
							print(tasksDisplayed.ToString());
						}

						break;
					case 3:
						print ("Case3");
						if (tasksDisplayed == 0) {
							taskDesc1.text = element.desc;
						} else if (tasksDisplayed == 1) {
							taskDesc2.text = element.desc;
						} else {
							taskDesc3.text = element.desc;
						}
						break;
					default:
						Debug.Log (tasksActive.ToString () + " tasks are active, only upto three tasks may be active");
						break;
					}
					element.displayed = true;
				}
			} else if (index == 0 && element.active == true) {
				taskDesc1.text = element.desc;
			}

			if (element.active == true && element.completed != true) {
				tasksDisplayed++;
			}
		}

		print(tasksDisplayed.ToString());
		tasksDisplayed = 0;

		if (tasksCompleted >= playerTasks.Length) {
			taskDesc1.text = "Go to boss' office";
			taskDesc2.text = "";
			taskDesc3.text = "";
			ending = true;
		}
	}
	public void printError (string message) {
		Debug.LogError (message);
	}
}