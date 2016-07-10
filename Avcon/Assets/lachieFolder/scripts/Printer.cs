using UnityEngine;
using System.Collections;

public class Printer : MonoBehaviour {

	public float spinSpeed;
	public float shotDelay = 0.6f;
	public float lifeTime;
	public Transform shotSpawn;
	public GameObject shot;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("Malfunction", 0f, shotDelay);
		StartCoroutine ("timeTillDestroy");
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (0,spinSpeed, 0);
	}

	void Malfunction()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
	}

	IEnumerator timeTillDestroy()
	{
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}
}
