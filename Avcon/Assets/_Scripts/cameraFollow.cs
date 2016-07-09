using UnityEngine;
using System.Collections;


[System.Serializable]

public class cameraFollow : MonoBehaviour {
	public float speed;

	public GameObject player;

	Vector3 offset;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position + offset, Time.deltaTime * speed);
	}
}


