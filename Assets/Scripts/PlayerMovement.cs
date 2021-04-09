using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	// WASD keys for input

	private CharacterController _playerCC;
	[Header("Controller Settings")]
	[Range(0,10)]
	[SerializeField] private float _speed;
	[SerializeField] private float _jumpHeight;
	[SerializeField] private float _gravityModifer;
	[Space]
	[Header("Camera Attributes")]
	[SerializeField] private float _mouseSensitivity;

	private Vector3 _direction;
	private Camera _mainCamera;
	

	private void Start()
	{
		_playerCC = GetComponent<CharacterController>();
		_mainCamera = Camera.main;
		LockCursor();
	}



	private void Update()
	{
		CalcuateMovement();
		CameraController();
		
		// Escape key to unlock cursor.
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}

	private void LockCursor()
	{
		//Lock cursor and hide
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void CameraController()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		// Apply mouseX movment to player roation y (look left and right).
		
		float lookRotation = mouseX * _mouseSensitivity * Time.deltaTime;
		Vector3 currentRotation = transform.localEulerAngles;
		currentRotation.y += lookRotation;

		transform.localEulerAngles = currentRotation;

		// Apply mouseY moveemnt to Camera rotationX value (look up down).
		
		float verticalLook = mouseY * _mouseSensitivity * Time.deltaTime;

		// Get the current camera rotation and apply mouseY movement.

		Vector3 cameraRotation =_mainCamera.transform.localEulerAngles;

		cameraRotation.x -= verticalLook;
		
		// Apply the rotation as Quaternion to the camera transform.

		_mainCamera.transform.localRotation = Quaternion.AngleAxis (cameraRotation.x, Vector3.right);
	}

	private void CalcuateMovement()
	{
		float yDirection = _direction.y;

		if (_playerCC.isGrounded)
		{
			// Z-Axis movement.
			float vertical = Input.GetAxis("Vertical");

			// X-Axis movemnt.
			float horizontal = Input.GetAxis("Horizontal");
			
			_direction = new Vector3(horizontal, yDirection, vertical);
			
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
			yDirection -= _gravityModifer * Time.deltaTime;
		}

		_direction.y = yDirection;
		Vector3 velocity = _direction * _speed;
		
		// Transform local space to world space
		velocity = transform.TransformDirection(velocity);

		_playerCC.Move(velocity * Time.deltaTime);
	}
}
