using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	
	public GameObject[] shootObjects;
	public float fireRate;
	public AudioSource Shoot;
	public GameObject playerController;

	int currentProjectileNum;
	GameObject currentProjectile;
	screenShake shakeScript;
	float timeToShoot;
	Transform firePoint;



	// Use this for initialization
	void Start () {
		displayProjectile ();
		playerController = GameObject.Find ("FPSController").transform.FindChild("FirstPersonCharacter").gameObject;
		firePoint = GameObject.Find ("FirePoint").transform;
		shakeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<screenShake> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > timeToShoot && Time.timeScale != 0) {
			timeToShoot = Time.time + 1 / fireRate;
			//shakeScript.Shake();
			shoot ();
		}

	}
		
	void shoot (){
		Shoot.Play ();
		print ("Shot");
		int random = Random.Range (0, 5);
		Instantiate(shootObjects[random], firePoint.position, shootObjects[random].transform.rotation);
	}

	void displayProjectile () {
		//currentProjectile = Instantiate (shootObjects [currentProjectileNum], firePoint.position, shootObjects[currentProjectileNum].transform.rotation) as GameObject;
		//currentProjectile.transform.parent = playerController.transform;
		//Destroy(currentProjectile.GetComponent<projectile>());
	}
}
