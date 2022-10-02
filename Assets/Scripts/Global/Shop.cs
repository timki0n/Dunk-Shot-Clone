using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[SerializeField]
	private BallsCollection _collection;

	[SerializeField]
	private Transform _parentItems;

	[SerializeField]
	private ItemBall _itemPrefab;

	private string _labelBall = "item_ball";

	private List<ItemBall> _items = new List<ItemBall>();

	private BallLoader _ballLoader;

	private void Start()
	{
		_ballLoader = FindObjectOfType<BallLoader>();

		CreateItems();
	}

	public void SelectBall(int ID)
	{
		_ballLoader.SetCurrentBallID(ID);

		for (int i = 0; i < _items.Count; i++)
		{
			if (ID == i)
				_items[i].Select();
			
			else
				_items[i].UnSelect();
		}
	}

	public void TryBuyBall(ItemBall item)
	{
		int money = MoneyManager.instance.GetMoney();
		if (money >= item.GetPrice())
		{
			MoneyManager.instance.SpendMoney(item.GetPrice());
			item.SetBallBought(true);
			SelectBall(item.GetID());
			BuyBall(item.GetID());
		}
	}

	private void CreateItems()
	{
		for (int i = 0; i < _collection.Balls.Length; i++)
		{
			ItemBall item = Instantiate(_itemPrefab, _parentItems);
			_items.Add(item);
			item.SetBallID(i);
			item.SetBallSprite(_collection.Balls[i].Sprite);
			item.SetPrice(_collection.Balls[i].Price);
			item.SetShop(this);
			if (i != 0)
				item.SetBallBought(CheckIsBoughtBall(i));
		}

		int currentID = _ballLoader.GetCurrentBallID();
		SelectBall(currentID);
	}

	private void BuyBall(int ID)
	{
		PlayerPrefs.SetString(_labelBall + ID.ToString(), "true");
		PlayerPrefs.Save();
	}

	private bool CheckIsBoughtBall(int ID)
	{
		bool isBought = Convert.ToBoolean(PlayerPrefs.GetString(_labelBall + ID.ToString(), "false"));
		return isBought;
	}
}
