using UnityEngine;
using System.Collections;

public class levelGenTest : MonoBehaviour {

	[Range(0,5)]
	public int roomsPerLevel;
	public Transform[] rooms;
	private Vector3 roomPos;
	public bool spawn;
	private int roomSelection;
	private int roomsInLevel;

	float z = 0;

	// Use this for initialization
	void Start () 
	{
		spawn = true;
		InvokeRepeating ("addRoom", 0f, .1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		roomSelection = Random.Range (0, rooms.Length);

		if (roomsInLevel >= roomsPerLevel) { //
			spawn = false;
		}
	}

	void addRoom()
	{
		if (spawn) {
			roomsInLevel += 1;
			roomPos = transform.position + new Vector3 (0,0,z* 35f);
			Instantiate(rooms[roomSelection],roomPos,Quaternion.identity);
			z++;
		}
	}
}
