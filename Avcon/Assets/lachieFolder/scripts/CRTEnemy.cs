using UnityEngine;
using System.Collections;

public class CRTEnemy : MonoBehaviour {

	public NavMeshAgent pathFinder;
	public Transform player;
	public Transform meleePos;
	public Rigidbody rb;
	public float radius;
	public float meleeSpeed = 3;
	public int meleeDamage = 5;
	public bool alert;
	public bool meleeHit;

	private float attackDistance = 1.7f;
	private float throwDistance;
	private float timeBetweenAttacks = 1f;
	private float nextAttackTime;
	private float collisionSize;
	private float playerCollisionSize;
	private float meleeDistance;
	private float weaponDistance;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		player = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		pathFinder = GetComponent<NavMeshAgent> ();
		collisionSize = GetComponent<CapsuleCollider> ().radius;
		playerCollisionSize = player.GetComponent<CharacterController> ().radius;
	}
	
	// Update is called once per frame
	void Update () 
	{
		meleeDistance = Vector3.Distance (transform.position, player.position);
		Alert ();
		Attack ();
		Chasing ();
	}


	void Alert()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position + transform.up, radius, 1 << LayerMask.NameToLayer ("Player"));
		alert = colliders.Length > 0;
		transform.LookAt (player.position - transform.up);

	}

	void Chasing()
	{
		Vector3 distanceFromPlayer = (player.position - transform.position).normalized;
		Vector3 targetPosition = player.position - distanceFromPlayer * (collisionSize + playerCollisionSize);
		pathFinder.SetDestination (targetPosition);
	}

	void Attack()
	{
		if (Time.time > nextAttackTime) {
			//hittingObject = Physics.Linecast (transform.position + transform.up, endPos.position, 1 << LayerMask.NameToLayer ("Obstacle"));
			if (meleeDistance < Mathf.Pow (attackDistance, 2)) {
				nextAttackTime = Time.time + timeBetweenAttacks;
				StartCoroutine ("meleeStopTime");
			}
		}
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
			}

			yield return null;
		}

		pathFinder.enabled = true;
	}
}
