using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {
	public float speed;
	public int damage;
	public float rotationSpeed;
	public PlayerShooting shootScript; 

	AudioSource smash;
	GameObject player;

	Vector3 force;
	void Start () {
		Physics.IgnoreLayerCollision (8, 8);
		Physics.IgnoreLayerCollision (8, 12);
		player = GameObject.FindWithTag ("Player");
		smash = GameObject.FindGameObjectWithTag ("Gm").GetComponent<AudioSource> ();
		shootScript = player.GetComponent<PlayerShooting> ();
	}

	void Awake() {
		player = GameObject.FindWithTag ("Player");
		force = player.transform.forward * speed;
		StartCoroutine ("selfDestruct");
	}
		
	void Update () {
		GetComponent<Rigidbody>().AddForce (force * Time.deltaTime);
		transform.Rotate( new Vector3 (transform.rotation.x + rotationSpeed * Time.deltaTime, transform.rotation.y, transform.rotation.z));
	}

	void OnTriggerEnter (Collider other) {
		smash.Play ();
		//take health
		if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
		}
		if (shootScript.currentProjectileNum == 4) {
			shootScript.currentProjectileNum = 0;
		} else if (shootScript.currentProjectileNum > 4) {
			Debug.LogError ("The 'currentProjectileNum' is out of range, look at projectile.cs and PlayerShooting.cs");
		} else {
			shootScript.currentProjectileNum++;
		}

		shootScript.displayProjectile ();
		Destroy(gameObject);
	}

	IEnumerator selfDestruct () {
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
	}
}
