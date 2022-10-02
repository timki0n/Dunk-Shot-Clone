using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private float _duration = 0.5f;

	[SerializeField]
	private GameObject[] _hideObjects;

	[SerializeField]
	private Transform _logoTransform;

	private void Start()
	{
		TouchManager.instance.OnTap += OnGameTap;
	}

	private void OnGameTap()
	{
		StopAllCoroutines();
		StartCoroutine(AnimateScaleDown());

		foreach (var obj in _hideObjects)
		{
			obj.SetActive(false);
		}
	}


	IEnumerator AnimateScaleDown()
	{
		for (float t = 0; t < 1; t+=Time.deltaTime / _duration)
		{
			_logoTransform.localScale = Vector3.Lerp(_logoTransform.localScale, Vector3.zero, Time.deltaTime / _duration);
			yield return null;
		}
		_logoTransform.localScale = Vector3.zero;
	}


}
