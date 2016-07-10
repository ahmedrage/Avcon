using UnityEngine;
using System.Collections;

public class enemyAttack : IEnemystate 
{
	private Enemy enemy;

	public void Execute()
	{
		Debug.Log ("short range state");
		//enemy.shortRange ();
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