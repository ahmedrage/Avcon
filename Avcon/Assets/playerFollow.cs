using UnityEngine;
using System.Collections;

public class playerFollow : MonoBehaviour {
	// This script may seem pointless to you, but it isn't, if you touch it we will all die
	Transform player;

	void Start () {
		player = transform.FindChild ("Player");
	}
	

	void Update () {
		transform.position = player.position;
		transform.rotation = player.rotation;
	}
}
