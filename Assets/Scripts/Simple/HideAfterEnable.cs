using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterEnable : MonoBehaviour
{
	[SerializeField]
	private float _delay = 1f;

	[SerializeField]
	private AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

	private Vector3 _startScale;

	private void Awake()
	{
		_startScale = transform.localScale;
	}

	private void OnEnable()
	{
		Invoke(nameof(Animate), _delay);
	}

	private void Animate()
	{
		if (gameObject.activeSelf)
			StartCoroutine(AnimateScaleDown());
	}

	IEnumerator AnimateScaleDown()
	{
		for (float t = 0; t < 1; t += Time.deltaTime)
		{
			transform.localScale = Vector3.LerpUnclamped(_startScale, Vector3.zero, _curve.Evaluate(t));
			yield return null;
		}
		gameObject.SetActive(false);
	}
}
