using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance = null;

	public event Action<int> OnScoreChanged;
	public event Action OnNewRecord;

	[SerializeField]
	private AudioSource _scoreSound; 

	private int _score;
	private int _combo;
	private int _scoreRecord;

	private string _scoreRecordLabel = "score_record";

	bool _isRecordNotified;

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

	private void InitializeManager()
	{
		_scoreRecord = PlayerPrefs.GetInt(_scoreRecordLabel);
		SetNewGame();
	}

	[ContextMenu("SET NULL RECORD")]
	public void SetNullRecord()
	{
		PlayerPrefs.SetInt(_scoreRecordLabel, 0);
		PlayerPrefs.Save();
	}

	public int GetScoreRecord()
	{
		return _scoreRecord;
	}

	public void SetNewGame()
	{
		_combo = 1;
		_score = 0;
		_isRecordNotified = false;
		OnScoreChanged?.Invoke(_score);
	}

	public void AddScores(int bounce, bool isClear)
	{
		if (isClear)
			_combo += 1;
		else
			_combo = 1;
		int plus = Mathf.Clamp(_combo * bounce, 1, 100);
		_score += plus;
		_scoreSound.pitch = 1 + (0.1f * plus);
		_scoreSound.Play();

		OnScoreChanged?.Invoke(_score);

		CheckEffect();

		CheckRecord();
	}

	private void CheckEffect()
	{
		if (_combo > 1)
			VibrationManager.instance.Vibrate();
	}

	private void CheckRecord()
	{
		if(_score > _scoreRecord)
		{
			_scoreRecord = _score;
			SaveRecord();

			if (!_isRecordNotified)
			{
				OnNewRecord?.Invoke();
				_isRecordNotified = true;
			}
		}
	}

	private void SaveRecord()
	{
		PlayerPrefs.SetInt(_scoreRecordLabel, _scoreRecord);
		PlayerPrefs.Save();
	}
}
