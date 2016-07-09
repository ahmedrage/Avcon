using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stressMeter : MonoBehaviour {
	Image stressBar;
	public float stress;
	// Use this for initialization
	void Start () {
		stressBar = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		stressBar.fillAmount = stress / 100;
	}
}
