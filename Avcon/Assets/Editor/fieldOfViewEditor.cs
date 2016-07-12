using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Enemy))]
public class fieldOfViewEditor : Editor {

	void OnSceneGUI()
	{
		Enemy AI = (Enemy)target;
		Handles.color = Color.white;
		Handles.DrawWireArc (AI.transform.position + AI.transform.up, Vector3.up, Vector3.forward, 360, AI.radius);
	}
}
