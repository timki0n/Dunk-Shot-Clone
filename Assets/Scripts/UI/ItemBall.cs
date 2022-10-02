using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBall : MonoBehaviour
{
	[SerializeField]
	private Button _button;

	[SerializeField]
	private Image _spriteBall;

	[SerializeField]
	private TextMeshProUGUI _textPrice;
	[SerializeField]
	private GameObject _objectPrice;

	[SerializeField]
	private GameObject _background;

	private Shop _shop;

	private bool _isBought = true;
	private bool _isSelect = false;
	private int _ballID;
	private int _price;

	private void Start()
	{
		_button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		if (_isBought)
		{
			_shop.SelectBall(_ballID);
		}
		else
		{
			_shop.TryBuyBall(this);
		}
			
	}

	public int GetID()
	{
		return _ballID;
	}

	public void SetPrice(int price)
	{
		_price = price;
		_textPrice.SetText(price.ToString());
	}

	public int GetPrice()
	{
		return _price;
	}

	public void SetShop(Shop shop)
	{
		_shop = shop;
	}

	public void SetBallID(int ID)
	{
		_ballID = ID;
	}

	public void SetBallBought(bool isBought)
	{
		_isBought = isBought;

		if (!isBought)
			_objectPrice.SetActive(true);
	}

	public void SetBallSprite(Sprite sprite)
	{
		_spriteBall.sprite = sprite;
	}

	public void Select()
	{
		_objectPrice.SetActive(false);
		_isSelect = true;
		_background.SetActive(true);
	}

	public void UnSelect()
	{
		_isSelect = false;
		_background.SetActive(false);
	}

	private void Update()
	{
		if (_isSelect)
			_spriteBall.transform.localScale = Vector3.one * ((Mathf.Sin(Time.time * 3) / 12) + 0.9f);
	}
}
