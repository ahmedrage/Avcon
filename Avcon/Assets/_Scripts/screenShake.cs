using UnityEngine;
using System.Collections;

public class screenShake : MonoBehaviour {
	//public static cameraShake Instance;
	public float _amplitude = 1f;
	public float _duration = 1;
	private Vector3 initialPosition;
	public bool isShaking = true;

	// Use this for initialization
	void Start () 
	{	
		//Instance = this;
		initialPosition = transform.localPosition;
	}

	public void Shake ()
	{	
		
		isShaking = true;
		CancelInvoke ();
		Invoke ("StopShaking", _duration);
	}

	public void StopShaking()
	{
		isShaking = false;
	}


	// Update is called once per frame
	void Update ()
	{
		if (isShaking && Time.timeScale != 0) 
		{
			initialPosition = transform.localPosition; 
			transform.localPosition = initialPosition + Random.insideUnitSphere* _amplitude;
		}
	}
}
