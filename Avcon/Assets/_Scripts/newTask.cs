using UnityEngine;
using System.Collections;

public class newTask : MonoBehaviour {
	public string desc;
	public int stress;
	public GameObject target;
	public AudioSource beep;
	Tasks taskScript;
	bool triggered;
	stressMeter stressScript;
	// Use this for initialization
	void Start () {
		taskScript = GameObject.FindGameObjectWithTag ("Gm").GetComponent<Tasks>();
		stressScript = GameObject.Find ("Background").GetComponent<stressMeter> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider Other) {
		addTask();
		triggered = true;
	}

	void addTask(){
		if (triggered == false && taskScript.tasksCompleted < 3) { 
			taskScript.tasksCompleted++;
			beep.Play ();
			taskScript.playerTasks [taskScript.tasksCompleted - 1].desc = desc;
			taskScript.playerTasks [taskScript.tasksCompleted - 1].stress = stress;
			taskScript.playerTasks [taskScript.tasksCompleted - 1].target = target;
			stressScript.stress += stress;
		}
	}
}