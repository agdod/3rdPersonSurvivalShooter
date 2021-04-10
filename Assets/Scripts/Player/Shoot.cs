using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	// Left click to fire.
	[SerializeField] private float _range = 100f;

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
		if (Physics.Raycast(rayOrigin, out hitInfo , _range))
		{
			Debug.DrawLine(rayOrigin.origin, hitInfo.point, Color.red);
			Debug.Log(" Hit object " + hitInfo.transform.name);
		}
		
	}
}
