using System.Collections;
using UnityEngine;

public class Net : MonoBehaviour
{
	[SerializeField]
	private BasketSetting _setting;

	[SerializeField]
	private Transform _viewTransform;

	public void Shake()
	{
		StopAllCoroutines();
		StartCoroutine(ShakeAnimate());
	}

	IEnumerator ShakeAnimate()
	{
		float sec = _setting.TimeShakeNet;
		float startY = _viewTransform.localScale.y;
		if (startY <= 1.1f)
			startY = 1.3f;
		for (float t = 0; t < 1; t+=Time.deltaTime / sec)
		{
			float y = Mathf.LerpUnclamped(startY, 1f, _setting.NetShakeCurve.Evaluate(t));
			_viewTransform.localScale = new Vector2(1f, y);
			yield return null;
		}
		_viewTransform.localScale = Vector3.one;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Shake();
	}
}
