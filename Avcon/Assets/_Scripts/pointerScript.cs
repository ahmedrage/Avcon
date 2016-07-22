using UnityEngine;
using System.Collections;

public class pointerScript : MonoBehaviour {
	Task currentTask;
	public GameObject target;
	Tasks tasksScript;
	GameObject player;

	// Use this for initialization
	void Start () {
		GameObject camera = GameObject.Find ("CameraSpaceCamera");
		player = GameObject.Find ("Player");
		tasksScript = GameObject.Find ("GM").GetComponent<Tasks> ();
		camera.SetActive (false);
		camera.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		checkTarget ();

	}
	void checkTarget () {
		currentTask = tasksScript.currentTask;
		if (tasksScript.tasksActive > 0 && tasksScript.currentTask != null && tasksScript.currentTask.taskObject != null) {

			if (currentTask.held == true) {
				target = currentTask.target;
			} else { 
				target = currentTask.taskObject;
			}
		}

		if (target != null) {
			Quaternion lookRotation = Quaternion.LookRotation (target.transform.position - player.transform.position);
			transform.rotation = lookRotation;
		}

	}
}
