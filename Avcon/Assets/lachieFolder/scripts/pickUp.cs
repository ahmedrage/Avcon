using UnityEngine;
using System.Collections;

public class pickUp : MonoBehaviour 
{	
	public int ammoType;
	public float range;

	GameObject player;
	Rigidbody rb;
	PlayerShooting shootScript;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		player = GameObject.Find ("FPSController");
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

		if (shootScript.currentProjectile == null) {
			//shootScript.displayProjectile (shootScript.ammoArray [0]);
		}
		shootScript.displayProjectile (shootScript.ammoArray [shootScript.ammo - 1]);
			Destroy (gameObject);
	}
}
