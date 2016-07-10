using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	
	public GameObject[] shootObjects;
	public float fireRate;
	public AudioSource Shoot;
	public GameObject playerController;
	public int currentProjectileNum;
	public bool enableCombat;
	public Transform firePoint;

	GameObject currentProjectile;
	screenShake shakeScript;
	float timeToShoot;




	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("FPSController").transform.FindChild("FirstPersonCharacter").gameObject;
		firePoint = GameObject.Find ("FirePoint").transform;
		shakeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<screenShake> ();
		if (enableCombat == true) {
			displayProjectile ();
		}

	}

	void Awake() {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > timeToShoot && Time.timeScale != 0 && enableCombat == true) {
			timeToShoot = Time.time + 1 / fireRate;
			shakeScript.Shake();
			shoot ();
		}

	}
		
	void shoot (){
		Shoot.Play ();
		print ("Shot");
		Instantiate(shootObjects[currentProjectileNum], firePoint.position, firePoint.rotation);
		if (currentProjectileNum >= 4) {
			currentProjectileNum = 0;
		} else {
			currentProjectileNum++;
		}
		displayProjectile ();
	}

	public void displayProjectile () {
		if (currentProjectile != null) {
			Destroy (currentProjectile);
		}
		currentProjectile = Instantiate (shootObjects [currentProjectileNum], firePoint.position, firePoint.rotation) as GameObject;
		currentProjectile.transform.parent = firePoint;
		//currentProjectile.transform.rotation = shootObjects [currentProjectileNum].transform.rotation;
		Destroy(currentProjectile.GetComponent<projectile>());
	}
}