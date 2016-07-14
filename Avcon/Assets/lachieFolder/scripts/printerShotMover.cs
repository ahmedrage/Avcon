using UnityEngine;
using System.Collections;

public class printerShotMover : MonoBehaviour {

	public float speed;
	public float rotateSpeed;
	public float lifeTime =2;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine ("timeTillDestroy");	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.forward * speed);
		transform.Rotate (0, 0, rotateSpeed);
	}

	void OnTriggerEnter(Collider other)
	{
		print ("Test");
		Destroy (gameObject);
	}

	IEnumerator timeTillDestroy()
	{
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}
}
