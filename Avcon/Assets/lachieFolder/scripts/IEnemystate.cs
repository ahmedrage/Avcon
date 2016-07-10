using UnityEngine;
using System.Collections;

public interface IEnemystate
{
	void Execute();
	void Enter (Enemy enemy);
	void Exit();
	void OnCollisionEnter (Collision other);
}
