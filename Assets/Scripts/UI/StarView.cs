using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textStars;

    [SerializeField]
    private Transform _centerTransform;

    [SerializeField]
    private AnimationCurve _shakeCurve;

    [SerializeField]
    [Range(0f, 1f)]
    private float _shakeRate = 0.5f;

    void Start()
    {
        if (MoneyManager.instance)
		{
            MoneyManager.instance.OnMoneyChanged += OnMoneyChanged;
            _textStars.SetText(MoneyManager.instance.GetMoney().ToString());
        }
    }

	private void OnMoneyChanged(int money)
	{
        _textStars.SetText(money.ToString());
        Shake();
    }

    [ContextMenu("SHAKE")]
    private void Shake()
	{
        StopAllCoroutines();
        StartCoroutine(AnimateShake());
    }

    IEnumerator AnimateShake()
	{
		for (float t = 0; t < 1; t+=Time.deltaTime)
		{
            _centerTransform.localScale = Vector3.LerpUnclamped(Vector3.one, Vector3.one * _shakeRate, _shakeCurve.Evaluate(t));
            yield return null;
        }
        _centerTransform.localScale = Vector3.one;

    }
}
