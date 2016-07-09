using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	public float range;
	public GameObject Player;
	public Transform Target;
	public int layerMask;
	AudioSource doorAudio;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		layerMask = 1 << 9;
		layerMask = ~layerMask;
		doorAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, Player.transform.position) < range) {
			Teleport();
		}


	}
		

	void Teleport() {
		if (Input.GetKeyDown(KeyCode.E)){
			doorAudio.Play ();
			Player.transform.position = Target.position;
		}
			
	}


}
