using UnityEngine;
using System.Collections;

public class pointerScript : MonoBehaviour {
	public GameObject target;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Hands");

	}
	
	// Update is called once per frame
	void Update () {
		Quaternion lookRotation = Quaternion.LookRotation (target.transform.position - player.transform.position);
		transform.rotation = lookRotation;
		//transform.LookAt(target.transform.position);
	}
}
