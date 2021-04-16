using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public enum State
	{
		Idle,
		Chase,
		Attack
	}

	[SerializeField] private float _speed;
	[SerializeField] private int _attackRate;
	[SerializeField] private int _attackDamage;
	[SerializeField] private State _state;

	private CharacterController _enemyCC;
	private GameObject _player;
	private Health _playerHealth;
	private float _gravity;

	private void Start()
	{
		_enemyCC = GetComponentInParent<CharacterController>();
		_player = GameObject.FindGameObjectWithTag("Player");
		_playerHealth = _player.GetComponent<Health>();
		_gravity = Physics.gravity.y;

		if (_player == null)
		{
			Debug.LogError("Player is Null.");
		}

		if (_playerHealth == null)
		{
			Debug.LogError("Player Health component is null.");
		}
		_state = State.Chase;
	}
	// Check if grounded
	// Calcuate direction = destination - source.
	// Calcuate velcoity
	// Apply gravity
	// Apply movement

	public State Status
	{
		get { return _state; }
		set { _state = value; }
	}

	private void Update()
	{
		if (_state == State.Chase)
		{
			EnemyMovement();
		}
	}

	void EnemyMovement()
	{
		Vector3 direction = Vector3.zero;
		Vector3 velocity;

		if (_enemyCC.isGrounded == true && _player != null)
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

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && _state != State.Attack)
		{
			_state = State.Attack;
			StartCoroutine(BeginAttack());
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_state = State.Chase;
			StopCoroutine(BeginAttack());
		}
	}

	private IEnumerator BeginAttack()
	{
		while (_state == State.Attack)
		{
			yield return new WaitForSeconds(_attackRate);
			if (_player != null)
			{
				_playerHealth.Damage(_attackDamage);
				Debug.Log("Attacking player");
			}
		}
	}

	public void StartAttack()
	{
		if (_state != State.Attack && _player != null)
		{
			_state = State.Attack;
			StartCoroutine(BeginAttack());
		}
	}

	public void StopAttacking()
	{
		_state = State.Chase;
		StopCoroutine(BeginAttack());
	}
}
