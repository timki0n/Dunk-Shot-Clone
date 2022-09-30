using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RecordText : MonoBehaviour
{
	private TextMeshProUGUI _text;

	private void Awake()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void OnEnable()
	{
		_text.SetText(ScoreManager.instance.GetScoreRecord().ToString());
	}
}
