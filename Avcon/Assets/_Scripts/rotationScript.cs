using UnityEngine;
using System.Collections;

public class rotationScript : MonoBehaviour {
	float camRayLength = 100f;
	Rigidbody playerRigidbody;
	public int mask;
	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody> ();
		mask = LayerMask.GetMask ("Environment");
	}
	
	// Update is called once per frame
	void Update () {
		Turning ();
	}

	void Turning ()
	{

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);


		RaycastHit floorHit;


		if(Physics.Raycast (camRay, out floorHit, camRayLength,mask))
		{

			Vector3 playerToMouse = floorHit.point - transform.position;


			playerToMouse.y = 0f;


			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);


			playerRigidbody.MoveRotation (newRotation);
		}
	}
}
