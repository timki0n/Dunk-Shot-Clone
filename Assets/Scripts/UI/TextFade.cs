using System.Collections;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshPro))]
public class TextFade : MonoBehaviour
{
	private TextMeshPro _text;

	[SerializeField]
	private float _upSideOffset;
	[SerializeField]
	private float _lifeTime = 0.5f;
	[SerializeField]
	private float _delay;

	[SerializeField]
	private AnimationCurve _curveOpacity;

	private Color _startColor;

	private void Awake()
	{
		_text = GetComponent<TextMeshPro>();
		_startColor = _text.color;
		SetClearColor();
		Invoke(nameof(Show), _delay);
	}

	public void SetText(string text)
	{
		_text.SetText(text);
	}

	private void Show()
	{
		
		_text.color = _startColor;

		StopAllCoroutines();
		StartCoroutine(AnimateFade());
		StartCoroutine(SlideUp());
	}

	private void SetClearColor()
	{
		_startColor.a = 0;
		_text.color = _startColor;
	}

	IEnumerator AnimateFade()
	{
		Color color = _startColor;
		for (float t = 0; t < 1; t += Time.deltaTime / _lifeTime)
		{
			color.a = _curveOpacity.Evaluate(t);
			_text.color = color;
			yield return null;
		}
		Destroy(gameObject);
	}

	IEnumerator SlideUp()
	{
		Vector2 from = transform.position;
		Vector2 to = (Vector2)transform.position + Vector2.up * _upSideOffset;
		for (float t = 0; t < 1; t += Time.deltaTime / _lifeTime)
		{
			transform.position = Vector2.Lerp(from, to, t);
			yield return null;
		}
	}
}
