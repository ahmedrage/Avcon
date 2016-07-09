using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[System.Serializable]
public class Task {
	public string desc = "";
	public int stress = 0;
	public GameObject target;
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
	}

	void setText () {
		
		if (playerTasks [0] != null) {
			taskDesc1.text = playerTasks [0].desc;
		}

		if (playerTasks [1] != null) {
			taskDesc2.text = playerTasks [1].desc;
		}

		if (playerTasks [2] != null) {
			taskDesc3.text = playerTasks [2].desc;
		}
			
	}


}
