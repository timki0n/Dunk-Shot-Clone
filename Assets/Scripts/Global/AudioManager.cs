using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance = null;

	[SerializeField]
	private AudioMixer _mixer;

	bool _isSound = true;

	private const float _lowerVolumeBound = -80.0f;
	private const float _upperVolumeBound = 0.0f;

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
	}

	private void Start()
	{
		Initialize();
	}

	public bool IsSound()
	{
		return _isSound;
	}

	public void SetSound(bool isSound)
	{
		_isSound = isSound;
		SetVolume();
		Save();
	}

	private void Initialize()
	{
		_isSound = Convert.ToBoolean(PlayerPrefs.GetString("is_sound", "true"));
		SetVolume();
	}

	private void SetVolume()
	{
		if (_isSound)
			_mixer.SetFloat("VolumeMaster", _upperVolumeBound);
		else
			_mixer.SetFloat("VolumeMaster", _lowerVolumeBound);
	}

	private void Save()
	{
		PlayerPrefs.SetString("is_sound", _isSound.ToString());
		PlayerPrefs.Save();
	}
}
