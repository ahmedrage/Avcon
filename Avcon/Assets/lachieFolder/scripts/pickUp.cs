using UnityEngine;
using System.Collections;

public class pickUp : MonoBehaviour 
{	
	public Transform handPos;

	private Rigidbody rb;
	private bool hasWeapon;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnMouseOver()
	{
		if (gameObject.tag == "PlayerPickUp") {
			PickUp ();
		}
	}

	void PickUp()
	{
		if (Input.GetButtonDown ("Fire1")) {
			if (!hasWeapon) {
				rb.useGravity = false;
				rb.isKinematic = true;
				this.transform.position = handPos.transform.position;
				this.transform.parent = handPos.transform;
				hasWeapon = true;
			} else {
				this.transform.parent = null;
				rb.isKinematic = false;
				rb.useGravity = true;
				hasWeapon = false;
			}
		}
	}
}
