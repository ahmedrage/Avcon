﻿using UnityEngine;
using System.Collections;

public class printerRotate : MonoBehaviour {

	public float Y;

	void Update () 
	{
		transform.Rotate (0, 30 * Time.deltaTime, 0);
		transform.position = new Vector3 (transform.position.x, Mathf.PingPong (Time.time * 0.5f, Y), transform.position.z);
	}
}