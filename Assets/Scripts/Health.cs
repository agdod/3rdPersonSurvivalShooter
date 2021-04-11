using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

	private void Start()
	{
		_currentHealth = _maxHealth;
	}

	public void Damage(int amount)
	{
		_currentHealth -= amount;
		if (_currentHealth < 1)
		{
			Debug.Log(gameObject.name + " is dead.");
			Destroy(this.gameObject);
		}
	}
}

