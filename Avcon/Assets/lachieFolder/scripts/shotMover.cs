using UnityEngine;
using System.Collections;

public class shotMover : MonoBehaviour {

	public float speed;
	public float timeTillDeath;
	private float rotateSpeed = 1;

	public Rigidbody rb;

	void Start () 
	{
		StartCoroutine ("lifeTime");
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.forward * speed);
		transform.Rotate (0, 0, 5 * rotateSpeed);
	}

	IEnumerator lifeTime()
	{
		yield return new WaitForSeconds (timeTillDeath);
		Destroy (gameObject);
	}

	void OnCollisionEnter ( Collision other)
	{
		if (other.gameObject.tag == "Player") {
			//rb.isKinematic = false;
			Destroy(this.gameObject);
			print ("player hit");
		}
	}
}

