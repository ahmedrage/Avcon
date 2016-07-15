using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class endLevel : MonoBehaviour {
	public float range;
	public GameObject Player;
	public Tasks taskScript;
	public int sceneToLoad;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		taskScript = GameObject.Find ("GM").GetComponent<Tasks> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(Player.transform.position, transform.position) < range && Input.GetButtonDown("Fire3") && taskScript.tasksCompleted >= taskScript.playerTasks.Length) {
			print ("Test");
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}
