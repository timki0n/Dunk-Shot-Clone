using UnityEngine;
using TMPro;

public class TextAnnouncer : MonoBehaviour
{
	[SerializeField]
	private string _plusLabel = "+%s";

	[SerializeField]
	private string _comboLabel = "Perfect %s";

	[SerializeField]
	private string _bounceLabel = "Bounce %s";

	[SerializeField]
	private float _yOffset;

	[Space(10)]
	[SerializeField]
	private TextFade _prefabPlus;
	[SerializeField]
	private TextFade _prefabCombo;
	[SerializeField]
	private TextFade _prefabBounce;

	private void Start()
	{
		ScoreManager.instance.OnScoreCombo += OnScoreCombo;
	}

	private void OnScoreCombo(int combo, int bounce, int plus)
	{
		Create(plus.ToString(), _plusLabel, _prefabPlus);
		TryCreateWithX(combo, _comboLabel, _prefabCombo);
		TryCreateWithX(bounce, _bounceLabel, _prefabBounce);
	}

	private void TryCreateWithX(int amount, string label, TextFade prefab)
	{
		if (amount > 0)
		{
			string textAmount = "!";
			if (amount > 1)
				textAmount = " x" + amount.ToString();

			Create(textAmount, label, prefab);
		}
	}

	private void Create(string amountText, string text, TextFade prefab)
	{
		TextFade textFade = Instantiate(prefab, GetSpawnPosition(), Quaternion.identity, transform);

		text = text.Replace("%s", amountText);
		textFade.SetText(text);
	}

	private Vector2 GetSpawnPosition()
	{
		Vector2 positionBasket = LevelManager.instance.GetCurrentBasketPosition();
		positionBasket += Vector2.up * _yOffset;
		return positionBasket;
	}
}
