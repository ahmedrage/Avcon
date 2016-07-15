using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {
	
	public bool enableCombat;
	public float fireRate;
	public float pickUpRange;
	public Transform CastStart;
	public Transform hands;
	public AudioSource pickupSound;
	public Image prompt;
	public LayerMask mask;
	public AudioSource Shoot;
	public GameObject[] shootObjects;
	public Sprite[] promptSprites;
	public bool infiniteAmmo;
	public int health = 150;

	[HideInInspector] public int[] ammoArray;
	[HideInInspector] public int ammo;
	[HideInInspector] public bool hasObject;
	[HideInInspector] public GameObject pickedObject;
	[HideInInspector] public GameObject objectInHands;

	GameObject currentProjectile;
	Transform firePoint;
	Rigidbody pickedRb;
	float timeToShoot;
	int currentProjectileNum;
	RaycastHit pickUpHit;
	screenShake shakeScript;

	void Start () {
		ammoArray = new int[3];
		firePoint = GameObject.Find ("FirePoint").transform;
		shakeScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<screenShake> ();
		Vector3 firePointPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane) );
		CastStart.position = firePointPosition;
	}

	void Awake() {
		
	}
	void Update () {
		if (health <= 0) {
			Application.LoadLevel (Application.loadedLevel);
		}

		rayCasting ();
		if (Time.time > timeToShoot && Time.timeScale != 0 && enableCombat == true && (ammo > 0 || infiniteAmmo == true) && (Input.GetButtonDown ("Fire1") || Input.GetAxis("Fire1") > 0) ) {
			shoot ();
		}
		if(pickUpHit.collider != null)
			print (pickUpHit.collider.gameObject.tag.ToString());
	}
		
	void shoot (){
		timeToShoot = Time.time + 1 / fireRate;
		shakeScript.Shake();
		Shoot.Play ();
		ammo--;
		if (currentProjectile != null) {
			Destroy (currentProjectile);
		}
		Instantiate(shootObjects[currentProjectileNum], firePoint.position, firePoint.rotation);

		if (ammo > 0) {
			displayProjectile (ammoArray [ammo - 1]);
		}
	}

	void rayCasting()
	{
		Vector3 forward = CastStart.TransformDirection (Vector3.forward);
		Physics.Raycast (CastStart.position, forward, out pickUpHit, pickUpRange, mask);
		Debug.DrawRay (CastStart.position, forward);

		if (pickUpHit.collider == null || pickUpHit.collider.tag != "Interact") {
			prompt.sprite = null;
			prompt.gameObject.SetActive (false);
		}
		if (pickUpHit.collider != null && pickUpHit.collider.gameObject.tag == "PlayerPickUp" && hasObject == false && (Input.GetButtonDown ("Fire1") || Input.GetAxis("Fire1") > 0) && pickUpHit.collider.gameObject.GetComponent<pickUp>().active == true) {
			pickUp (true);
		} 
		else if (hasObject == true && (Input.GetButtonDown ("Fire2") || Input.GetAxis("Fire2") > 0)) {
			pickUp (false);
		}

		if (pickUpHit.collider != null){
			pickedObject = pickUpHit.collider.gameObject;

			if (pickUpHit.collider.tag == "Interact") {
				prompt.gameObject.SetActive (true);
				prompt.sprite = promptSprites [0];
			}
		}
	}

	public void pickUp(bool pickingUp) {
		if (pickedObject != null && pickedObject.GetComponent<pickUp>().active == true){
			if (pickingUp == true) {
				pickedObject = pickUpHit.collider.gameObject;
				
				pickedRb = pickedObject.GetComponent<Rigidbody> ();
				pickedObject.transform.position = hands.position;
				pickedObject.transform.parent = hands;
				pickupSound.Play ();
			} else {
				pickedObject.transform.parent = null;
			}
		}
		hasObject = pickingUp;
	}

	public void displayProjectile (int ProjectileNum) {
		if (currentProjectile != null) {
			Destroy (currentProjectile);
		}
		currentProjectileNum = ProjectileNum;
		currentProjectile = Instantiate (shootObjects [ammoArray [ammo - 1]], GameObject.Find("displayPlace").transform.position, firePoint.rotation) as GameObject;
		currentProjectile.transform.parent = firePoint;
		Destroy(currentProjectile.GetComponent<projectile>());
	}
		
}