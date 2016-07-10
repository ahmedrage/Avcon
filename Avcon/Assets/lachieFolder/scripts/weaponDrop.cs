using UnityEngine;
using System.Collections;

public class weaponDrop : MonoBehaviour {

	public GameObject[] weapons;
	public GameObject currentWeapon;
	public float weaponTime;
	public bool pickedUp;
	public Vector3 changedPos;

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
		transform.position = changedPos;
		yield return new WaitForSeconds (weaponTime);
		Instantiate (currentWeapon, transform.position, transform.rotation);
		pickedUp = false;
	}
}
