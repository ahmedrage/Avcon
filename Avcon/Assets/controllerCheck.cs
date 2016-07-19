using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class controllerCheck : MonoBehaviour {

	public GameObject pressToStart;
	public GameObject Ximg;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		Input.GetJoystickNames ();
		print(Input.GetJoystickNames ().Length);


		if (Input.GetJoystickNames ().Length == 1) {
			pressToStart.SetActive (true);
		} else {
			pressToStart.SetActive (false);
		}

	}
}
