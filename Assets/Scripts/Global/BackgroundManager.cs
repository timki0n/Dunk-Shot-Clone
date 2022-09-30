using System;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	public static BackgroundManager instance = null;
	public event Action<bool> OnChangedDarkMode;

	[SerializeField]
	private GameObject _darkObject;

	private string _isDarkLabel = "is_dark";

	private bool _isDark;

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
		_isDark = Convert.ToBoolean(PlayerPrefs.GetString(_isDarkLabel, "false"));
		_darkObject.SetActive(_isDark);
	}

	public bool IsDarkMode()
	{
		return _isDark;
	}

	public void SetDarkMode(bool isDark)
	{
		_isDark = isDark;
		_darkObject.SetActive(_isDark);
		OnChangedDarkMode?.Invoke(_isDark);
		Save();
	}

	private void Save()
	{
		PlayerPrefs.SetString(_isDarkLabel, _isDark.ToString());
		PlayerPrefs.Save();
	}
}
