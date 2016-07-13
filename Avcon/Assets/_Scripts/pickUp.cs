using UnityEngine;
using System.Collections;

public class pickUp : MonoBehaviour 
{	
	public bool completed;
	public bool active;
	public bool colliding;
	public Transform target;
	public PlayerShooting shootScript;

	// Use this for initialization
	void Start () 
	{
		shootScript = GameObject.Find ("Player").GetComponent<PlayerShooting> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (shootScript.hasObject == true) {
			GetComponent<Rigidbody> ().velocity = shootScript.hands.position - transform.position;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.transform == target && active == true) {
			completed = true;
		}
	}

	void OnCollisionStay() {
		colliding = true;
	}

	void OnCollisionExit() {
		colliding = false;
	}
}
