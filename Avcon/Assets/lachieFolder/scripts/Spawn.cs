using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public Wave[] waves;
	public GameObject Enemy;

	GameObject[] enemiesRemaining;
	Wave currentWave;
	int currentWaveNumber;

	int enemiesRemainingToSpawn;
	float nextSpawnTime;

	void Start(){
		NextWave ();
		StartCoroutine ("enemyCheck");
	}

	void Update () {
		enemiesRemaining = GameObject.FindGameObjectsWithTag ("Enemy");
		print (enemiesRemaining.Length);
		if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
			enemiesRemainingToSpawn--;
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
			Vector3 position = new Vector3 (Random.Range(-9.0f, 5.0f), 0, Random.Range(2.0f, 13.0f));
			Instantiate (Enemy ,position , Quaternion.identity );
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
		float checkRate = 0.75f;
		while (true) {
			if (enemiesRemaining.Length == 0) {
				NextWave ();
			}
			yield return new WaitForSeconds (checkRate);
		}
	}
}
