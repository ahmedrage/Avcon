using UnityEngine;
using System.Collections;

public class DialougeChanger : MonoBehaviour {
    public int changer;
    public Dialouge dialouge;
	// Use this for initialization
	void Start () {
        dialouge = GameObject.Find("DialougeField").GetComponent<Dialouge>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        print("1");
        if (other.gameObject.tag == "Player")
        {
            print("2");
            dialouge.phraseType = changer;
        }
    }
}
