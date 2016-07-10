using UnityEngine;
using System.Collections;

public class enemyPatrol : IEnemystate 

{
	private Enemy enemy;

	public void Start()
	{
		enemy.Patrol ();
	}

	public void Execute()
	{
		if (enemy.pathFinder.remainingDistance < 0.5f) {
			enemy.Patrol ();
		}
	}
		
	public void Enter (Enemy enemy)
	{
		this.enemy = enemy;
	}
	public void Exit()
	{

	}
	public void OnCollisionEnter (Collision other)
	{
		
	}
}
