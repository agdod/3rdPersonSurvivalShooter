using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int _maxHealth;
	[SerializeField] private int _currentHealth;
	[SerializeField]
	public bool Dead { get; private set; }
	public delegate void Deactivate();
	public event Deactivate deactivate;

	private void Start()
	{
		_currentHealth = _maxHealth;
		deactivate += OnDeactivate;
		Dead = false;
	}

	private void OnDisable()
	{
		deactivate -= OnDeactivate;
	}

	public void Damage(int amount)
	{
		_currentHealth -= amount;
		if (_currentHealth < 1)
		{
			Debug.Log(gameObject.name + " is dead.");
			Dead = true;
			if (deactivate != null)
			{
				deactivate();
			}
		}
	}

	private void OnDeactivate()
	{
		// Add small delay to allow any subscribers to process.
		
		Invoke("DisableThis", 0.1f);
	}

	private void DisableThis()
	{
		this.gameObject.SetActive(false);
	}
}

