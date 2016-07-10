using UnityEngine;
using System.Collections;

public class jamScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Input.mousePosition;
	}

	void OnCollisionEnter (Collision other) {
		print ("Jam Collision detected");
	}
}
