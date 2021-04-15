using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	// Left click to fire.
	[SerializeField] private float _range = 100f;
	[SerializeField] private int _damage;
	[SerializeField] private GameObject _hitEffect;
	[SerializeField] private PoolManager _poolManager;

	private int _layerMask = 1 << 8;

	private void Start()
	{
		// inverse the bit mask.
		_layerMask = ~_layerMask;
	}

	private void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			FireRayCast();
		}
	}

	private void FireRayCast()
	{
		// Fire ray from center of screen (crosshair positon).

		Vector3 crossHairPoint = new Vector3(0.5f, 0.5f, 0f);
		Ray rayOrigin = Camera.main.ViewportPointToRay(crossHairPoint);
		RaycastHit hitInfo;
		if (Physics.Raycast(rayOrigin, out hitInfo, _range,_layerMask))
		{
			Debug.DrawLine(rayOrigin.origin, hitInfo.point, Color.red);
			Debug.Log(" Hit object " + hitInfo.transform.name);
			if (hitInfo.collider.TryGetComponent(out Health health))
			{
				VisualEffects(hitInfo);
				health.Damage(_damage);
			}
		}
	}

	private void VisualEffects(RaycastHit hitInfo)
	{
		// Instantiate blood splat effects at raycast postion.
		// Rotate towards the hit normal postion (surface normal).
		// NOTE : BloodSpat effect is Recycled, the recycling is controlled by BloodSplat itself.

		Debug.Log("blood spat.");
		GameObject go = _poolManager.RequestFromPool("BloodSplat");
		go.transform.position = hitInfo.point;
		go.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
	}
}