using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	
	public GameObject[] shootObjects;
	public int[] ammoArray;
	public int ammo;
	public float fireRate;
	public AudioSource Shoot;
	public int currentProjectileNum;
	public bool enableCombat;
	public float pickUpRange;
	public GameObject currentProjectile;

	Transform playerCamera;
	screenShake shakeScript;
	float timeToShoot;
	RaycastHit pickCast;
	GameObject playerController;
	Transform firePoint;


	// Use this for initialization
	void Start () {
		ammoArray = new int[3];
		playerController = GameObject.Find ("FPSController").transform.FindChild("FirstPersonCharacter").gameObject;
		firePoint = GameObject.Find ("FirePoint").transform;
		shakeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<screenShake> ();
		playerCamera = transform.FindChild ("FirstPersonCharacter").FindChild("CastStart");
	}

	void Awake() {
		
	}
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > timeToShoot && Time.timeScale != 0 && enableCombat == true) {
			timeToShoot = Time.time + 1 / fireRate;
			shakeScript.Shake();
			shoot ();
		}
			
	}
		
	void shoot (){
		if (ammo > 0) {
			if (currentProjectile != null) {
				Destroy (currentProjectile);
			}
			Shoot.Play ();
			print ("Shot");
			Instantiate(shootObjects[currentProjectileNum], firePoint.position, firePoint.rotation);
			ammo--;
			if (ammo > 0) {
				displayProjectile (ammoArray [ammo - 1]);
			}
		}
	}

	public void displayProjectile (int ProjectileNum) {
		if (currentProjectile != null) {
			Destroy (currentProjectile);
		}
		currentProjectileNum = ProjectileNum;
		currentProjectile = Instantiate (shootObjects [ammoArray [ammo - 1]], firePoint.position, firePoint.rotation) as GameObject;
		currentProjectile.transform.parent = firePoint;
		//currentProjectile.transform.rotation = shootObjects [currentProjectileNum].transform.rotation;
		Destroy(currentProjectile.GetComponent<projectile>());
	}
}