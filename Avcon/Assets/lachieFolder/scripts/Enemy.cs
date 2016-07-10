using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour // buggy as fuck
{
	[Range(0,10)]
	public float radius;
	public float timeTillSeen;
	public float meleeDistance;
	public float weaponDistance;
	//public float distanceToThrow; 
	public float distanceToMelee;
	public float waitTimeRanged;
	public float meleeSpeed;
	public int health;
	public int damageAmount;
	public bool seen; 
	public bool alert; 
	public bool sightLine;
	public bool hasWeapon;
	public bool pathUpdating;
	public Rigidbody rb;
	public NavMeshAgent pathFinder;
	public Transform[] patrolPoints;
	public Transform endPos;
	public Transform gunPos; 
	public Transform player;
	public Transform shotSpawn;
	public Vector3 prevPlayerPos;
	public weaponDrop Drop;
	public GameObject weapon;
	//public GameObject deathParticle;

	private IEnemystate currentState;
	private int points = 0;

	// new melee attack test
	float attackDistanceThreshold = 1.5f;
	float timeBetweenAttacks = 1;
	float nextAttackTime;
	float collisionSize;
	float playerCollisionSize;


	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		pathFinder = GetComponent<NavMeshAgent> ();
		collisionSize = GetComponent<CapsuleCollider> ().radius;
		playerCollisionSize = player.GetComponent<CapsuleCollider> ().radius;
		rb.isKinematic = true;
		toState (new enemyPatrol ());
		seen = true;
		Drop = GameObject.Find ("weaponDrop").GetComponent<weaponDrop> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentState.Execute ();
		meleeDistance = Vector3.Distance (transform.position, player.position);
		weaponDistance = Vector3.Distance (transform.position, gunPos.position);

		if (Drop.pickedUp == false) {
			Weaponsearch ();
		}

		Seen ();
		lineOfSight ();
		Destroy ();
		Attack ();
		Pickup ();
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
		}/* else {
			radius = 3;
			timeTillSeen = 2;
		}*/

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
		pathUpdating = true;	
		if (pathUpdating) {
			//Vector3 directionToPlayer = (player.position = transform.position).normalized;
			//Vector3 playerPosition = player.position - directionToPlayer *(collisionSize + playerCollisionSize);
			pathFinder.SetDestination (player.position);
			alert = false;
			prevPlayerPos = player.position;
			transform.LookAt (player.position - transform.up);
		}
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

		if (other.gameObject.tag == "Shot") {
			health -= damageAmount;
		}
	}

	public void Pickup()
	{
		if (hasWeapon == true) {
			weapon.transform.position = shotSpawn.transform.position;
			weapon.transform.rotation = shotSpawn.transform.rotation;
			weapon.transform.parent = shotSpawn.transform;
		} else {
			weapon.transform.parent = null;
		}
	}
		
	public void lineOfSight()
	{
		sightLine = Physics.Linecast (transform.position + transform.up, endPos.position, 1 << LayerMask.NameToLayer ("Player"));
		Debug.DrawLine (transform.position + transform.up, endPos.position, Color.green);
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

	public void Destroy()
	{
		if (health <= 0) {
			Destroy (gameObject);
			//Instantiate (deathParticle, transform.position, transform.rotation); when a particle is instantiated does it get destroyed? Because if not we are going to need to have a corutine to destroy them(after death).
			//StartCoroutine("particleDestroy");
		}
	}

	public void lastSeen()
	{
		pathFinder.SetDestination (prevPlayerPos);
		StartCoroutine ("timeSearching");
	}

	public void Attack()
	{
		if (hasWeapon == true && seen == true && sightLine == true/* && meleeDistance >= distanceToThrow*/) {
					toState(new enemyWeapon());
			}

		/*if (seen == true && meleeDistance <= distanceToMelee) {
			toState (new enemyAttack ());
		}*/

		if (Time.time > nextAttackTime) {
			if (meleeDistance < Mathf.Pow (attackDistanceThreshold, 2) && seen == true) {
				nextAttackTime = Time.time + timeBetweenAttacks;
				StartCoroutine ("meleeStopTime");
			}
		}
	}

	/*public void shortRange()
	{
		hasWeapon = false;
		StartCoroutine ("meleeStopTime");
	}*/

	public void longRange()
	{
		pathFinder.speed = 0;
		weapon.GetComponent<shotMover> ().enabled = true;
		StartCoroutine ("rangedStopTime");
	}

	IEnumerator rangedStopTime()
	{	
		yield return new WaitForSeconds (waitTimeRanged);
		hasWeapon = false;
		pathFinder.speed = 4f;
	}

	IEnumerator meleeStopTime()
	{
		pathUpdating = false;
		pathFinder.enabled = false;

		Vector3 currentPos = transform.position;
		Vector3 targetPos = player.position;

		float percent = 0;

		while (percent <= 1) {

			percent += Time.deltaTime * meleeSpeed;
			float interpolation = (-Mathf.Pow(percent,2)+ percent)*4;
			transform.position = Vector3.Lerp (currentPos, targetPos, interpolation);

			yield return null;
		}

		pathUpdating = true;
		pathFinder.enabled = true;
	}

	IEnumerator timeSearching()
	{
		yield return new WaitForSeconds (1f);
		toState (new enemyPatrol ());
	}

	/*IEnumerator particleDestroy()
	{
		yield return new WaitForSeconds ();
		Destroy (deathParticle);
	}*/
}				