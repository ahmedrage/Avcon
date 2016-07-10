using UnityEngine;
using System.Collections;

public class enemyWeapon : IEnemystate
{
	private Enemy enemy;

	public void Execute()
	{
		enemy.longRange ();
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
