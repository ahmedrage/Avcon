using UnityEngine;
using System.Collections;

public class pickUp : MonoBehaviour 
{	
	public bool completed;
	public bool active;
	public bool colliding;
	public bool toPlace;
	public float speedToHands = 10;
	public Transform target;
	public PlayerShooting shootScript;
	Vector3 firstPosition;

	// Use this for initialization
	void Start () 
	{
		shootScript = GameObject.Find ("Player").GetComponent<PlayerShooting> ();
		firstPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.parent != null) {
			GetComponent<Rigidbody> ().velocity = (shootScript.hands.position - transform.position) * speedToHands;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.transform == target && active == true) {
			if (toPlace = true) {
				other.transform.GetChild (0).gameObject.SetActive (true);
			}
			completed = true;
		}
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "killFloor") {
			respawn ();
		}
	}

	void OnCollisionStay() {
		colliding = true;
	}

	void OnCollisionExit() {
		colliding = false;
	}

	public void respawn () {
		transform.position = firstPosition;
	}
}
