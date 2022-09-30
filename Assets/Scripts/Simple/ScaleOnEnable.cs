using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnEnable : MonoBehaviour
{
	[SerializeField]
	[Range(0f, 1f)]
	private float _delay = 0.1f;

	[SerializeField]
	private AnimationCurve _curve;

	private Vector3 _startScale;

	private void Awake()
	{
		_startScale = transform.localScale;
	}

	private void OnEnable()
	{
		transform.localScale = Vector3.zero;
		Invoke(nameof(Animate), _delay);
	}

	private void Animate()
	{
		if (gameObject.activeSelf)
			StartCoroutine(AnimateScaleUp());
	}

	IEnumerator AnimateScaleUp()
	{
		for (float t = 0; t < 1; t+=Time.deltaTime)
		{
			transform.localScale = Vector3.LerpUnclamped(Vector3.zero, _startScale, _curve.Evaluate(t));
			yield return null;
		}
		transform.localScale = _startScale;
	}
}
