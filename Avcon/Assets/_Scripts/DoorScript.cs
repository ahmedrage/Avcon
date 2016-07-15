using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	public float range;
	public GameObject Player;
	public Transform Target;
	public bool transformToTarget; // if true the player is teleporting to the target, otherwise he is teleporting from the target to the transform connected to the script
	public bool destroyPreviousRoom;
	public GameObject previousRoom;

	AudioSource doorAudio;

	void Start () {
		Player = GameObject.Find ("Player");
		doorAudio = GetComponent<AudioSource> ();
	}
	void Update () {
		if (Vector3.Distance (transform.position, Player.transform.position) < range && Input.GetButtonDown("Fire3")) {
			transformToTarget = true;
			Teleport();
		} else if (Vector3.Distance (Target.position, Player.transform.position) < range && Input.GetButtonDown("Fire3") && destroyPreviousRoom == false) {
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
			if (Player.transform.FindChild("FirstPersonCharacter").FindChild("Hands").childCount > 0) {
				Player.transform.FindChild("FirstPersonCharacter").FindChild("Hands").GetChild(0).position = Target.position;
			}
		} else if (transformToTarget == false && destroyPreviousRoom == false ) {
			print ("Teleporting to origin");
			Player.transform.position = transform.position;
			if (Player.transform.FindChild("FirstPersonCharacter").FindChild("Hands").childCount > 0) {
				Player.transform.FindChild("FirstPersonCharacter").FindChild("Hands").GetChild(0).position = transform.position;
			}
		}

		if (destroyPreviousRoom == true && previousRoom != null) {
			Destroy (previousRoom);
		}
	}
}