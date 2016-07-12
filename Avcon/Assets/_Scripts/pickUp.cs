using UnityEngine;
using System.Collections;

public class pickUp : MonoBehaviour 
{	
	public bool completed;
	public bool active;
	public Transform target;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.transform == target && active == true) {
			completed = true;
		}
	}
}
