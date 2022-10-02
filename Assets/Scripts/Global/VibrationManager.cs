using System;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
	public static VibrationManager instance = null;

	private string _canVibrateLabel = "can_vibrate";

	private bool _canVibrate;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance == this)
		{
			Destroy(gameObject);
		}

		InitializeManager();
	}

	private void Start()
	{
		if(ScoreManager.instance)
			ScoreManager.instance.OnScoreCombo += OnScoreCombo;
	}

	private void OnScoreCombo(int combo, int bounce, int plus)
	{
		if (combo > 0)
			Vibrate();
	}

	private void InitializeManager()
	{
		Vibration.Init();
		Load();
	}

	public void SetCanVibrate(bool canVibrate)
	{
		_canVibrate = canVibrate;
		Save();
	}

	public bool CanVibrate()
	{
		return _canVibrate;
	}

	public void Vibrate()
	{
		if (!_canVibrate) return;

		Debug.Log("VIBRATION");
		Vibration.VibratePeek();
	}

	private void Load()
	{
		_canVibrate = Convert.ToBoolean(PlayerPrefs.GetString(_canVibrateLabel, "true"));
	}

	private void Save()
	{
		PlayerPrefs.SetString(_canVibrateLabel, _canVibrate.ToString());
		PlayerPrefs.Save();
	}
}
