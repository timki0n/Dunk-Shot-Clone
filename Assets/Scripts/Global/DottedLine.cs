using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DottedLine : MonoBehaviour
{
    [SerializeField]
    private Sprite _dot;

    [SerializeField]
    private string _sortingLayerName = "Ball";

    [SerializeField]
    private Color _colorSprite;

    [SerializeField]
    [Range(0.01f, 1f)]
    private float _size;

    [SerializeField]
    private int _count;

    private List<Vector2> _positions = new List<Vector2>();
    private List<SpriteRenderer> _dots = new List<SpriteRenderer>();


	private void Start()
	{
        CreateDots();

		TouchManager.instance.OnRelease += Hide;
        TouchManager.instance.OnTap += Show;
    }

    private void CreateDots()
	{
        for (int i = 0; i < _count; i++)
        {
            SpriteRenderer s = GetOneDot();
            _dots.Add(s);
        }
    }

	private void Hide(Vector2 vector, float dist)
	{
        StopAllCoroutines();
        StartCoroutine(FadeAnimateTo(0f, 0.5f));
	}

    private void Show()
	{
        StopAllCoroutines();
        StartCoroutine(FadeAnimateTo(1f, 0.2f));
    }


    SpriteRenderer GetOneDot()
    {
        var gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * _size;
        gameObject.transform.parent = transform;

        var sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = _dot;
        sr.sortingLayerName = _sortingLayerName;
        sr.color = _colorSprite;
        return sr;
    }

    public void DrawDottedLine(List<Vector2> positions)
    {
        _positions = positions;

        Render();
    }

    private void Render()
    {
		for (int i = 0; i < _dots.Count; i++)
		{
            if (i >= _positions.Count)
                break;
            Transform dT = _dots[i].transform;
            dT.position = Vector2.Lerp(dT.position, _positions[i], Time.deltaTime * 10f);
		}
    }

    IEnumerator FadeAnimateTo(float amount, float sec)
	{
        if (_dots.Count == 0)
            yield break;

        Color startColor = _dots[0].color;
        Color newColor = startColor;

        for (float t = 0; t < 1; t+=Time.deltaTime / sec)
		{
            float alpha = Mathf.Lerp(startColor.a, amount, t);
            newColor.a = alpha;
            SetColorDots(newColor);
            yield return null;
		}
        newColor.a = amount;
        SetColorDots(newColor);
    }

    void SetColorDots(Color color)
	{
        foreach (SpriteRenderer sprite in _dots)
        {
            sprite.color = color;
        }
    }
}
