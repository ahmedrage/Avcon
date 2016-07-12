using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour 
{	
	public int ammoType;
	public float range;

	public GameObject player;
	Rigidbody rb;
	public PlayerShooting shootScript;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		player = GameObject.Find ("Player");
		shootScript = player.GetComponent<PlayerShooting> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance (player.transform.position, transform.position) < range && shootScript.ammo < 3) {
			PickUp ();
		}
	}

	void PickUp()
	{
		shootScript.ammo++;
		shootScript.ammoArray [shootScript.ammo - 1] = ammoType - 1;
		shootScript.displayProjectile (shootScript.ammoArray [shootScript.ammo - 1]);
			Destroy (gameObject);
	}
}
