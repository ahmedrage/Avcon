using UnityEngine;
using System.Collections;

public class firstEncounter : MonoBehaviour {

	public Transform player;
	public float playerToEnemyDistance;
	public float distanceToSlow; 
	public bool slowTime;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerToEnemyDistance = Vector3.Distance (transform.position, player.position);

		if (playerToEnemyDistance <= distanceToSlow) {
			SloDown ();
		}
	}

	void SloDown()
	{
		Time.timeScale = 0.3f;
		// display prompt
		StartCoroutine("slowMoStop");
		// start coroutine
		//put time back to normal
	}

	IEnumerator slowMoStop()
	{
		yield return new WaitForSeconds (1f);
		Time.timeScale = 1;
	}
}
