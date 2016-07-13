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

	private Vector3 changedPos;
	public bool hit;


	// Use this for initialization
	void Start () 
	{
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
	}
	void OnTriggerEnter ( Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			pickedUp = true;
			StartCoroutine ("timeTillNextWeapon");
		}
	}

	IEnumerator timeTillNextWeapon ()
	{
		yield return new WaitForSeconds (weaponTime);
		Instantiate (currentWeapon, transform.position, transform.rotation);
		maxItemsPerRoom++;
		pickedUp = false;
		StopCoroutine ("timeTillNextWeapon");
	}
}
