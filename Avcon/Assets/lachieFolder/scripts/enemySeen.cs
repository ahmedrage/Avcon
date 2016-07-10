using UnityEngine;
using System.Collections;

public class enemySeen : IEnemystate 
{
	private Enemy enemy;

	public void Execute()
	{
		enemy.Alert ();
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
