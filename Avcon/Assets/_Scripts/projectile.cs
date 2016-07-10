using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {
	public float speed;
	public int damage;
	public float rotationSpeed;
	public PlayerShooting shootScript; 

	AudioSource smash;
	GameObject player;
	Vector3 projectileVelocity;
	void Start () {
		shootScript = player.GetComponent<PlayerShooting> ();
		Physics.IgnoreLayerCollision (8, 8);
		Physics.IgnoreLayerCollision (8, 12);
		player = GameObject.FindWithTag ("Player");
		smash = GameObject.FindGameObjectWithTag ("Gm").GetComponent<AudioSource> ();
	}

	void Awake() {
		player = GameObject.FindWithTag ("Player");
		StartCoroutine ("selfDestruct");
		projectileVelocity = GameObject.Find ("FirePoint").transform.forward * speed;
	}
		
	void Update () {
		GetComponent<Rigidbody> ().velocity = projectileVelocity;
		transform.Rotate( new Vector3 (transform.rotation.x + rotationSpeed * Time.deltaTime, transform.rotation.y, transform.rotation.z));

	}

	void OnTriggerEnter (Collider other) {
		smash.Play ();
		//take health
		if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
		}
		Destroy (gameObject);
	}

	IEnumerator selfDestruct () {
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
	}
}
