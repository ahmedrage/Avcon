using UnityEngine;
using System.Collections;

public class pointerScript : MonoBehaviour {
	public GameObject target;
	Tasks tasksScript;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Hands");
		tasksScript = GameObject.Find ("GM").GetComponent<Tasks> ();
	}
	
	// Update is called once per frame
	void Update () {
		checkTarget ();

	}
	void checkTarget () {
		target = tasksScript.currentTask.taskObject;
		if (tasksScript.tasksActive > 0 && tasksScript.currentTask != null && tasksScript.currentTask.taskObject != null) {
			Quaternion lookRotation = Quaternion.LookRotation (target.transform.position - player.transform.position);
			transform.rotation = lookRotation;
		}
	}
}
