using UnityEngine;
using System.Collections;

public class playerShotMover : MonoBehaviour {

	public float speed;
	public float timeTillDeath;

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
	}

	IEnumerator lifeTime()
	{
		yield return new WaitForSeconds (timeTillDeath);
		Destroy (gameObject);
	}

	void OnCollisionEnter ( Collision other)
	{
		if (other.gameObject.tag == "Enemy") {
			print ("enemy hit");
			//Destroy (other.gameObject);
		}
	}
}
