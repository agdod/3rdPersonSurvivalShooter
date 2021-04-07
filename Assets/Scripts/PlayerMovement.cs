using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	// Get handle to character controller.
	// WASD keys for input
	// input system, horizontal,vertical)
	// direction = vector to move
	// velocity = direction * speed
	// if is grounded
	// if jump
	// velocity = new velocity with added y
	// Controllewr.move(velocity * time.deltatime)

	private CharacterController _playerCC;
	[Header("Player Attributes")]
	[Range(0,10)]
	[SerializeField] private float _speed;
	[SerializeField] private float _jumpHeight;
	[SerializeField] private float _jumpForce;

	[Space]
	[Header("General Attributes")]
	[SerializeField] private float _gravityModifer;

	private Vector3 _direction;


	private void Start()
	{
		_playerCC = GetComponent<CharacterController>();
	}

	private void Update()
	{
		Movement();
	}

	private void Movement()
	{
		// Z-Axis movement.
		float vertical = Input.GetAxis("Vertical");
		// X-Axis movemnt.
		float horizontal = Input.GetAxis("Horizontal");
		float yDirection=_direction.y;
		_direction = new  Vector3(horizontal, yDirection, vertical);
		
		if (_playerCC.isGrounded)
		{
			// Y-Axis movemnt.
			if (Input.GetKeyDown(KeyCode.Space))
			{
				// Jump.
				yDirection = _jumpHeight;
				Debug.Log("Jump.");
			}
			
		} 
		else
		{
			yDirection -=  _gravityModifer;
		}
		_direction.y = yDirection;
		Vector3 velocity = _direction * _speed;	
		_playerCC.Move(velocity * Time.deltaTime);
	}
}
