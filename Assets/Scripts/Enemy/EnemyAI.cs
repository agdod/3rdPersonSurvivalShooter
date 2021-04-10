using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private float _speed;

	private CharacterController _enemyCC;
	private GameObject _player;
	private float _gravity;

	private void Start()
	{
		_enemyCC = GetComponent<CharacterController>();
		_player = GameObject.FindGameObjectWithTag("Player");
		_gravity = Physics.gravity.y;
	}
	// Check if grounded
	// Calcuate direction = destination - source.
	// Calcuate velcoity
	// Apply gravity
	// Apply movement

	private void Update()
	{
		EnemyMovement();
	}

	void EnemyMovement()
	{
		Vector3 direction = Vector3.zero;
		Vector3 velocity ;

		if (_enemyCC.isGrounded == true)
		{
			direction = _player.transform.position - transform.position;
			direction.Normalize();
			direction.y = 0;

			// Rotate to face player
			transform.localRotation = Quaternion.LookRotation(direction);

			
		}

		// Apply gravity.
		direction.y += _gravity;

		// Apply the movement.
		velocity = direction * _speed * Time.deltaTime;
		_enemyCC.Move(velocity);
	}
}
