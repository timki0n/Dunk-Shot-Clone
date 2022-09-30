using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
	public static MoneyManager instance = null;

	public event Action<int> OnMoneyChanged;

	[SerializeField]
	private AudioSource _soundCollect;

	private string _moneyLabel = "money";

	private int _money; 

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
		Load();
	}

	public int GetMoney()
	{
		return _money;
	}

	public void AddMoney()
	{
		_money += 1;
		_soundCollect.Play();
		OnMoneyChanged?.Invoke(_money);
		Save();
	}

	private void Load()
	{
		_money = PlayerPrefs.GetInt(_moneyLabel, 0);
	}

	private void Save()
	{
		PlayerPrefs.SetInt(_moneyLabel, _money);
		PlayerPrefs.Save();
	}
}
