using UnityEngine;
using System.Collections;

public class closeIn : MonoBehaviour {

	public MeshRenderer mesh;
	public BoxCollider box;
	public GameObject objects;
	public Light spot;
	public Light redSpot;

	// Use this for initialization
	void Start () 
	{
		mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		box = GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			StartCoroutine ("boxDestroyDelay");
		}
	}

	void Flicker()
	{
		
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

	}
}
