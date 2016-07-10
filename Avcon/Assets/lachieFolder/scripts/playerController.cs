using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerController : MonoBehaviour 
{
	public float speed;
	public float timeToFire = 0;
	public float fireRate;
	public GameObject shot;
	public Transform shotSpawn;
	private Rigidbody rb;

	Camera viewCamera;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		viewCamera = Camera.main;
	}
	
	void Update()
	{
		Movement ();
	}

	void Movement()
	{
		float moveVertical = Input.GetAxisRaw ("Vertical");
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = (movement * speed);

		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayDistance;

		if (groundPlane.Raycast(ray,out rayDistance)){
			Vector3 point = ray.GetPoint(rayDistance);
			LookAt(point);
		}

		if (Input.GetButton ("Fire1")) {
			Shooting ();
		}
	}

	 void LookAt (Vector3 lookPoint)
	{
		Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt (heightCorrectedPoint);
	}

	void Shooting()
	{
		if (Time.time > timeToFire) {
			timeToFire = Time.time + 1 / fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}	
	}
}
	