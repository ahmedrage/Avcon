using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class bossDeath : MonoBehaviour {

	public GameObject death;
	public bool bossDead;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "playerShot") {
			Instantiate (death, transform.position+transform.up, Quaternion.identity);
			Destroy (gameObject);
			bossDead = true;
		}
	}
}
