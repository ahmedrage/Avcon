using UnityEngine;
using System.Collections;

public class weaponDrop : MonoBehaviour {

	public GameObject[] weapons;
	public GameObject currentWeapon;
	public float weaponTime;
	public float maxItemsPerRoom;
	public float radius;
	public float MaxX;
	public float MinX;
	public float MaxZ;
	public float MinZ;
	public float Y;
	public bool pickedUp;
	public bool hit;


	private Vector3 changedPos;
	private float numOfItems;
	private bool canSpawn;


	// Use this for initialization
	void Start () 
	{
		canSpawn = true;
		currentWeapon = weapons [Random.Range (0, weapons.Length)];
		Instantiate(currentWeapon,transform.position,transform.rotation);
	}
	
	// Update is called once per frame
	void Update () 
	{
		changedPos = new Vector3 (Random.Range (MinX,MaxX), Y, Random.Range (MinZ,MaxZ));
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius, 1 << LayerMask.NameToLayer("Obstacle"));

		hit = colliders.Length > 0;

		if (hit == true || pickedUp == true) {
			transform.position = changedPos;
		}

		if (numOfItems >= maxItemsPerRoom) {
			canSpawn = false;
		}
	}
	void OnTriggerEnter ( Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			pickedUp = true;
			if (canSpawn) {
				StartCoroutine ("timeTillNextWeapon");
			}
		} else {
			pickedUp = false;
		}
	}

	IEnumerator timeTillNextWeapon ()
	{
		yield return new WaitForSeconds (weaponTime);
		Instantiate (currentWeapon, transform.position, transform.rotation);
		numOfItems++;
		pickedUp = false;
		StopCoroutine ("timeTillNextWeapon");
	}
}
