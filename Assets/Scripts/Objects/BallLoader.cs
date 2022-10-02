using UnityEngine;

public class BallLoader : MonoBehaviour
{
	[SerializeField]
	private BallsCollection _collection;

	[SerializeField]
	private SpriteRenderer _renderer;

	[SerializeField]
	private AudioSource _soundTap;

	private string _labelBallId = "ball_id";

	private int _currentID;

	private void Awake()
	{
		Load();
	}

	private void Start()
	{
		SetBallSettings();
	}

	public void SetCurrentBallID(int ID)
	{
		if (ID < 0 || ID >= _collection.Balls.Length) return;

		_currentID = ID;
		SetBallSettings();
		Save();
	}

	public int GetCurrentBallID()
	{
		return _currentID;
	}

	private void SetBallSettings()
	{
		_renderer.sprite = _collection.Balls[_currentID].Sprite;
		_soundTap.clip = _collection.Balls[_currentID].AudioTap;
		_soundTap.pitch = _collection.Balls[_currentID].PitchAudioTap;
	}

	private void Load()
	{
		_currentID = PlayerPrefs.GetInt(_labelBallId, 0);
	}

	private void Save()
	{
		PlayerPrefs.SetInt(_labelBallId, _currentID);
		PlayerPrefs.Save();
	}
}
