using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
	// BloodSpalt Fx fades over x seconds after y seconds of being enbaled.
	// Total time Fx is active is x+y 

	[SerializeField] private float _fadeOutDuration;
	[SerializeField] private float _timeToLive;
	private float _timeElapsed;
	private SpriteRenderer _spriteRenderer;

	public delegate void BubbleRecycle(GameObject obj);
	public BubbleRecycle bubbleRecycle;

	private void OnEnable()
	{
		Invoke("OnRecycle", _timeToLive);
	}

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnRecycle()
	{
		if (this.gameObject.activeInHierarchy == true)
		{
			StartCoroutine(FadeOutEffect());
		}
	}

	IEnumerator FadeOutEffect()
	{
		float alphaValue;
		while (_timeElapsed < _fadeOutDuration)
		{
			alphaValue = Mathf.Lerp(1, 0, _timeElapsed / _fadeOutDuration);
			_timeElapsed += Time.deltaTime;
			_spriteRenderer.color = new Color(255, 255, 255, alphaValue);
			yield return new WaitForEndOfFrame();
		}
		alphaValue = 0;
		// Bubble the delegate up
		bubbleRecycle(this.gameObject);
	}

	public void OnTargetDestroyed()
	{
		bubbleRecycle(this.gameObject);
	}
}
