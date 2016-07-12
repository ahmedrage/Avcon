using UnityEngine;
using System.Collections;

public class objectKinematic : MonoBehaviour {

	public Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Enemy") {
			rb.isKinematic = true;
			rb.useGravity = false;
		}
	}
}
