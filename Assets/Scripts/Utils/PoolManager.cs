using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	[System.Serializable]
	class ObjectPool
	{
		[SerializeField] private string _name;
		[SerializeField] private GameObject _objectPrefab;
		[SerializeField] private GameObject _objectContainer;
		[SerializeField] private int _amount;
		[SerializeField] private List<GameObject> _pool;

		public List<GameObject> Pool
		{
			get { return _pool; }
			private set {; }
		}

		public string Name
		{
			get { return _name; }
		}

		public int Amount
		{
			get { return _amount; }
		}

		public Transform Container
		{
			get { return _objectContainer.transform; }
		}

		public GameObject RequestObject()
		{
			// Loop through pool, checking for inactive object.
			// On finding inactive object set active and return.
			int index;
			for (index = 0; index < _pool.Count; index++)
			{
				if (_pool[index].activeInHierarchy == false)
				{
					_pool[index].SetActive(true);
					return _pool[index];
				}
			}

			// On reaching this point all current Objects are currently active.
			// None found, insantiate new one, add to list, setActive and return to player.

			IncreasePool(true);
			return _pool[index];
		}

		public void IncreasePool(bool isActive)
		{
			GameObject go = Instantiate(_objectPrefab);
			if (_objectContainer != null)
			{
				go.transform.parent = _objectContainer.transform;
			}
			_pool.Add(go);
			go.SetActive(isActive);
		}
	}

	[SerializeField] private List<ObjectPool> _objectPoolList = new List<ObjectPool>();

	private void Start()
	{
		PoolSetup();
	}

	// Setup pooled objects.
	private void PoolSetup()
	{
		foreach (ObjectPool obj in _objectPoolList)
		{
			for (int i = 0; i < obj.Amount; i++)
			{
				obj.IncreasePool(false);
			}
		}
	}

	public GameObject RequestFromPool(string name)
	{
		// Passs in string name of Pool.
		// Retrun frist <ObjectPool> that matches name.
		bool foundPool = false;
		foreach (ObjectPool obj in _objectPoolList)
		{
			if (obj.Name == name)
			{
				foundPool = true;
				return obj.RequestObject();
			}
		}
		if (foundPool == false)
		{
			Debug.LogError("No <ObjectPool> <" + name + "> was found. Unable to return Object.");
		}
		return null;
	}

	public void RecycleObject(string pool, GameObject obj)
	{
		// Return object to objectPool,
		// Return back to Object Container.
		// Disable the object.

		foreach (ObjectPool objPool in _objectPoolList)
		{
			if (objPool.Name == pool)
			{
				if (objPool.Container != null)
				{
					obj.transform.parent = objPool.Container;
					obj.SetActive(false);
				}
			}
		}
	}
}
