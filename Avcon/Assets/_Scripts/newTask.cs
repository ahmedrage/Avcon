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
		print ("triggered");
		addTask();
		triggered = true;
		print ("triggered");
	}

	void addTask(){
		
		print ("func'd");
		if (triggered == false && taskScript.taskNumber < 3) { 
			taskScript.taskNumber++;
			beep.Play ();
			taskScript.playerTasks [taskScript.taskNumber - 1].desc = desc;
			taskScript.playerTasks [taskScript.taskNumber - 1].stress = stress;
			taskScript.playerTasks [taskScript.taskNumber - 1].target = target;

			stressScript.stress += stress;
		}
	}
}
