using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class Enemy : MonoBehaviour // buggy as fuck
{
	[Range(0,10)]
	public float radius;
	public float timeTillSeen;
	public float waitTimeRanged;
	public float meleeSpeed;
	public float vibrateTime = 0.3f;
	public int health;
	public int damageAmount;
	public int meleeDamage = 1;
	public bool seen; 
	public bool alert; 
	public bool sightLine;
	public bool hasWeapon;
	public bool blocked;
	public bool meleeHit;
	public Rigidbody rb;
	public NavMeshAgent pathFinder;
	public Transform[] patrolPoints;
	public Transform endPos;
	public Transform throwPos;
	public Transform meleePos;
	public Transform gunPos; 
	public Transform player;
	public Transform shotSpawn;
	public Vector3 prevPlayerPos;
	public weaponDrop Drop;
	public GameObject deathParticle;
	public GameObject weapon;
	public Material normal;
	public Material hit;

	private IEnemystate currentState;
	private bool hittingObject;
	private int points = 0;
	private float attackDistance = 1.7f;
	private float throwDistance;
	private float timeBetweenAttacks = 1;
	private float nextAttackTime;
	private float collisionSize;
	private float playerCollisionSize;
	private float meleeDistance;
	private float weaponDistance;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		pathFinder = GetComponent<NavMeshAgent> ();
		player = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		//gunPos = GameObject.FindWithTag("Drop").GetComponent<Transform>();
		//Drop = GameObject.FindWithTag("Drop").GetComponent<weaponDrop>();
		collisionSize = GetComponent<CapsuleCollider> ().radius;
		playerCollisionSize = player.GetComponent<CharacterController> ().radius;
		rb.isKinematic = true;
		toState (new enemyPatrol ());
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		currentState.Execute ();
		meleeDistance = Vector3.Distance (transform.position, player.position);
		weaponDistance = Vector3.Distance (transform.position, gunPos.position);

		if (Drop.pickedUp == false) {
			Weaponsearch ();
		}

		Seen ();
		lineOfSight ();
		Attack ();
		Pickup ();
		obstacleCheck ();

		if (health <= 0) {
			Instantiate (deathParticle, transform.position, transform.rotation); 
			Destroy (gameObject);
		}
	}
		
	public void toState(IEnemystate nextState)
	{
		if (currentState != null) {
			currentState.Exit ();
		}
		currentState = nextState;
		currentState.Enter (this);
	}

	public void Patrol()
	{
		if (patrolPoints.Length == 0) {
			return;
		}
		pathFinder.destination = patrolPoints [points].position;
		points = (points + 1) % patrolPoints.Length;
	}
		
	public void Seen()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position + transform.up, radius, 1 << LayerMask.NameToLayer ("Player"));
		alert = colliders.Length > 0;

		if (alert == true) {
			radius = 7;
			timeTillSeen -= Time.deltaTime;
		}

		if (timeTillSeen <= 0.0f) {
			seen = true;
		} else {
			seen = false;
			toState (new enemyPatrol ());
		}

		if (seen == true) {
			pathFinder.speed = 4.5f;
			toState (new enemySeen ());
		}
	}

	public void Alert()
	{
		if (hasWeapon) {
			throwDistance = 2;
		} else {
			throwDistance = 0;
		}

		Vector3 distanceFromPlayer = (player.position - transform.position).normalized;
		Vector3 targetPosition = player.position - distanceFromPlayer * (collisionSize + playerCollisionSize + throwDistance);
		pathFinder.SetDestination (targetPosition);
		prevPlayerPos = player.position;
		transform.LookAt (player.position - transform.up);
	}

	public void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Player"){
			toState (new enemySeen ());
		}

		if (other.gameObject.tag == "Pickup" && hasWeapon == false) {
			hasWeapon = true;
			weapon = other.gameObject;
		}


	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "playerShot" && other.gameObject.GetComponent<projectile>() != null) {
			gameObject.GetComponent<Renderer>().material = hit;
			StartCoroutine ("shotFlash");
		}
	}

	public void Pickup()
	{
		if (weapon != null) {
			if (hasWeapon == true) {
				weapon.transform.position = shotSpawn.transform.position;
				weapon.transform.rotation = shotSpawn.transform.rotation;
				weapon.transform.parent = shotSpawn.transform;
			} else {
				weapon.transform.parent = null;
			}
		}
	}
		
	public void lineOfSight()
	{
		sightLine = Physics.Linecast (transform.position + transform.up, endPos.position, 1 << LayerMask.NameToLayer ("Player"));
		Debug.DrawLine (transform.position + transform.up, endPos.position, Color.green);
	}

	public void obstacleCheck()
	{
		hittingObject = Physics.Linecast (transform.position + transform.up, throwPos.position, 1 << LayerMask.NameToLayer ("Obstacle"));
		if (hittingObject) {
			blocked = true;
			print ("hiting object");
		} else {
			blocked = false;
		}
	}

	public void Weaponsearch()
	{
		if (weaponDistance < meleeDistance && hasWeapon == false && seen == true && gunPos != null) {
			pathFinder.SetDestination (gunPos.position);
		}
		else if( weaponDistance <= 3 && hasWeapon == false && seen == false && gunPos != null){
			pathFinder.SetDestination (gunPos.position);
			toState(new enemyPatrol());
		}
	}

	public void lastSeen()
	{
		pathFinder.SetDestination (prevPlayerPos);
		StartCoroutine ("timeSearching");
	}

	public void Attack()
	{
		if (hasWeapon == true && seen == true && sightLine == true && blocked == false) {
				toState(new enemyWeapon());
			}

		if (Time.time > nextAttackTime) {
			hittingObject = Physics.Linecast (transform.position + transform.up, endPos.position, 1 << LayerMask.NameToLayer ("Obstacle"));
			if (meleeDistance < Mathf.Pow (attackDistance, 2) && seen == true && hasWeapon == false && blocked == false) {
				nextAttackTime = Time.time + timeBetweenAttacks;
				StartCoroutine ("meleeStopTime");
			}
		}
	}

	public void longRange()
	{
		 // this could be the cancer bug. that stops the player.
		if (weapon != null) {
			pathFinder.speed = 0;
			weapon.GetComponent<shotMover> ().enabled = true;
			StartCoroutine ("rangedStopTime");
		}
	}

	IEnumerator rangedStopTime()
	{	
		yield return new WaitForSeconds (waitTimeRanged);
		hasWeapon = false;
		pathFinder.speed = 4f;
	}

	IEnumerator meleeStopTime()
	{
		pathFinder.enabled = false;

		Vector3 currentPos = transform.position;
		Vector3 distanceFromPlayer = (player.position - transform.position).normalized;
		Vector3 targetPos = player.position - distanceFromPlayer * (collisionSize);

		float percent = 0;

		while (percent <= 1) {
			percent += Time.deltaTime * meleeSpeed;
			float interpolation = (-Mathf.Pow(percent,2)+ percent)*4;
			transform.position = Vector3.Lerp (currentPos, targetPos, interpolation);
			meleeHit = Physics.Linecast (transform.position + transform.up, meleePos.position, 1 << LayerMask.NameToLayer ("Player"));

			if (meleeHit) {
				player.GetComponent<PlayerShooting> ().health -= meleeDamage;
				meleeHit = false;
				GamePad.SetVibration (PlayerIndex.One, 100, 100);
				StartCoroutine ("vibrationTime");
			}

			yield return null;
		}

		pathFinder.enabled = true;
	}

	IEnumerator timeSearching()
	{
		yield return new WaitForSeconds (1f);
		toState (new enemyPatrol ());
	}
		
	IEnumerator shotFlash()
	{
		yield return new WaitForSeconds (0.1f);
		gameObject.GetComponent<Renderer> ().material = normal;
	}

	IEnumerator vibrationTime()
	{
		yield return new WaitForSeconds (vibrateTime);
		GamePad.SetVibration (PlayerIndex.One, 0, 0);
	}
}				