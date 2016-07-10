using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	
	public GameObject[] shootObjects;
	public float fireRate;
	public AudioSource Shoot;
	public GameObject playerController;
	public int currentProjectileNum;

	GameObject currentProjectile;
	screenShake shakeScript;
	float timeToShoot;
	Transform firePoint;



	// Use this for initialization
	void Start () {
		//
		playerController = GameObject.Find ("FPSController").transform.FindChild("FirstPersonCharacter").gameObject;
		firePoint = GameObject.Find ("FirePoint").transform;
		shakeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<screenShake> ();
	}

	void awake() {
		displayProjectile ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > timeToShoot && Time.timeScale != 0) {
			timeToShoot = Time.time + 1 / fireRate;
			shakeScript.Shake();
			shoot ();
		}

	}
		
	void shoot (){
		displayProjectile ();
		Shoot.Play ();
		print ("Shot");
		Instantiate(shootObjects[currentProjectileNum], firePoint.position, shootObjects[currentProjectileNum].transform.rotation);
	}

	public void displayProjectile () {
		if (currentProjectile != null) {
			Destroy (currentProjectile);
		}
		currentProjectile = Instantiate (shootObjects [currentProjectileNum], firePoint.position, shootObjects[currentProjectileNum].transform.rotation) as GameObject;
		currentProjectile.transform.parent = playerController.transform.FindChild("FirePoint");
		currentProjectile.transform.rotation = shootObjects [currentProjectileNum].transform.rotation;
		Destroy(currentProjectile.GetComponent<projectile>());
	}
}