using UnityEngine;
using System.Collections;

public class weaponDrop : MonoBehaviour {

	public GameObject[] weapons;
	public GameObject currentWeapon;
	public float weaponTime;
	public float radius;
	public bool pickedUp;

	private Vector3 changedPos;
	private bool hit;


	// Use this for initialization
	void Start () 
	{
		currentWeapon = weapons [Random.Range (0, weapons.Length)];
		Instantiate(currentWeapon,transform.position,transform.rotation);
	}
	
	// Update is called once per frame
	void Update () 
	{
		changedPos = new Vector3 (Random.Range (-4,9), -3.3f, Random.Range (-6, 6));
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius, 1 << LayerMask.NameToLayer("Obstacle"));

		hit = colliders.Length > 0;

		if (hit == true) {
			transform.position = changedPos;
		}
	}
	void OnTriggerEnter ( Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			pickedUp = true;
			Destroy (other.gameObject);
			StartCoroutine ("timeTillNextWeapon");
		}
	}

	IEnumerator timeTillNextWeapon ()
	{
		transform.position = changedPos;
		yield return new WaitForSeconds (weaponTime);
		Instantiate (currentWeapon, transform.position, transform.rotation);
		pickedUp = false;
	}
}
