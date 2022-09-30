using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
	[SerializeField]
	private Color _colorInGame;

	[SerializeField]
	private Color _colorInMenu;

	private TextMeshProUGUI _text;

	private void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		ScoreManager.instance.OnScoreChanged += OnScoreChanged;
		LevelManager.instance.OnLoseGame += OnLoseGame;
	}

	private void OnLoseGame()
	{
		_text.color = _colorInMenu;
	}

	private void OnScoreChanged(int score)
	{
		_text.SetText(score.ToString());
	}
}
