using UnityEngine;
using System.Collections;

public class fearLightingChange : MonoBehaviour {

	public Color fear;
	public Light spot;
	public Enemy enemy;
	public float changeRate;
	// Use this for initialization
	void Start () 
	{
		spot = GetComponent<Light> ();
		enemy = GameObject.FindWithTag ("Enemy").GetComponent<Enemy> ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (enemy.seen) {
			spot.color = Color.Lerp (spot.color, fear, changeRate * Time.deltaTime);
		}
	
	}
}
