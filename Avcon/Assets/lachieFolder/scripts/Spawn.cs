﻿using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public Wave[] waves; 
	public GameObject Enemy;
	public float max_X; 
	public float min_X; 
	public float max_Z; 
	public float min_Z; 

	GameObject[] enemiesRemaining;
	Wave currentWave;
	Vector3 changedPos;
	int currentWaveNumber;
	int enemiesRemainingToSpawn;
	float nextSpawnTime;
	float radius = 1.5f;
	bool hit;

	void Start(){
		NextWave ();
		StartCoroutine ("enemyCheck");
	}

	void Update () {

		changedPos = new Vector3 (Random.Range (min_X, max_X), -3.5f, Random.Range (min_Z, max_Z));
		
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius, 1 << LayerMask.NameToLayer ("Obstacle"));
		hit = colliders.Length > 0;

		if (hit == true) {
			transform.position = changedPos;
		}

		enemiesRemaining = GameObject.FindGameObjectsWithTag ("Enemy");
		print (enemiesRemaining.Length);

		if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
			enemiesRemainingToSpawn--;
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
			Instantiate (Enemy, transform.position, Quaternion.identity);
			transform.position = changedPos;
		}
	}

	void NextWave (){
		currentWaveNumber ++;
		if (currentWaveNumber - 1 < waves.Length) {
			currentWave = waves [currentWaveNumber - 1];
			enemiesRemainingToSpawn = currentWave.enemyCount;
		}
	}

	[System.Serializable]
	public class Wave {
		public int enemyCount;
		public float timeBetweenSpawns;
	}

	IEnumerator enemyCheck()
	{
		float checkRate = 0.25f;
		while (true) {
			if (enemiesRemaining.Length == 0) {
				NextWave ();
			}
			yield return new WaitForSeconds (checkRate);
		}
	}
}