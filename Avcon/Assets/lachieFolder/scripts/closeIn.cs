using UnityEngine;
using System.Collections;

public class closeIn : MonoBehaviour {

	public MeshRenderer mesh;
	public BoxCollider box;
	public BoxCollider blockBox;
	public BoxCollider blockBox2;
	public GameObject objects;
	public CharacterController player;
	public Light spot;
	public Light redSpot;

	// Use this for initialization
	void Start () 
	{
		mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		player = GameObject.FindWithTag ("Player").GetComponent<CharacterController> ();

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			StartCoroutine ("boxDestroyDelay");
		}
	}

	IEnumerator boxDestroyDelay()
	{
		yield return new WaitForSeconds (0.5f);
		mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		Destroy (redSpot);
		box.isTrigger = false;
		yield return new WaitForSeconds (1.5f);
		spot.enabled = true;
		yield return new WaitForSeconds (0.2f);
		spot.enabled = false;
		yield return new WaitForSeconds (0.3f);
		spot.enabled = true;
		yield return new WaitForSeconds (0.2f);
		spot.enabled = false;
		yield return new WaitForSeconds (0.3f);
		spot.enabled = true;
		objects.SetActive (true);
		blockBox.isTrigger = true;
		blockBox2.isTrigger = true;
		Destroy (this);

	}
}
