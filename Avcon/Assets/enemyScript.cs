using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform);
		GetComponent<Rigidbody> ().velocity = transform.forward* 2;
	}
}
