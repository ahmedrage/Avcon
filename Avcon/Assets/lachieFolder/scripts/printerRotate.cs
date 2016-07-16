using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class printerRotate : MonoBehaviour {

	public float Y;
	public Light spotlight;

	void Update () 
	{
		transform.Rotate (0, 30 * Time.deltaTime, 0);
		transform.position = new Vector3 (transform.position.x, Mathf.PingPong (Time.time * 0.5f, Y), transform.position.z);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			spotlight.enabled = true;
			SceneManager.LoadScene ("mainMenu");
		} 		
	}
}
