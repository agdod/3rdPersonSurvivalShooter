using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
	private EnemyAI _enemyAI;

	private void Start()
	{
		_enemyAI = transform.parent.GetComponentInChildren<EnemyAI>();
	
		if (_enemyAI == null)
		{
			Debug.LogError("EnemyAi is null!");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_enemyAI.StartAttack();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_enemyAI.StopAttacking();
		}
	}
}
