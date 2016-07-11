﻿using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	public float range;
	public GameObject Player;
	public Transform Target;
	public int layerMask;
	public bool transformToTarget; // if true the player is teleporting to the target, otherwise he is teleporting from the target to the transform connected to the script

	AudioSource doorAudio;

	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		layerMask = 1 << 9;
		layerMask = ~layerMask;
		doorAudio = GetComponent<AudioSource> ();
	}
	void Update () {
		if (Vector3.Distance (transform.position, Player.transform.position) < range && Input.GetKeyDown(KeyCode.E)) {
			transformToTarget = true;
			Teleport();
		} else if (Vector3.Distance (Target.position, Player.transform.position) < range && Input.GetKeyDown(KeyCode.E)) {
			print ("Test");
			transformToTarget = false;
			Teleport ();
		}
	}
	void Teleport() {
			doorAudio.Play ();
		if (transformToTarget == true) {
			print ("Teleporting to target");
			Player.transform.position = Target.position;
		} else {
			print ("Teleporting to origin");
			Player.transform.position = transform.position;
		}
	}
}