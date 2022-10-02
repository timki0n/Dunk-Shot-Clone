using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance = null;

	public event System.Action OnLoseGame;

	[SerializeField]
	private Basket _basketPrefab;

	[SerializeField]
	private Coin _coinPrefab;

	[SerializeField]
	private Basket _startBasket;

	[SerializeField]
	private Transform _loseTransform;

	[Header("Respawn Ball Offset")]
	[SerializeField]
	float _respawnYOffset = 0.5f;

	[Header("Trigger Lose Offset Y")]
	[SerializeField]
	float _loseYOffset = 6f;

	[Header("Max Baskets At Same Time")]
	[SerializeField]
	private int _maxBaskets = 2;

	[Header("Random Spawn Position")]
	[SerializeField]
	private float _minYOffset = 2f;
	[SerializeField]
	private float _maxYOffset = 5f;

	[Space(10)]
	[SerializeField]
	[Range(0f, 1f)]
	private float _minXOffset;

	[SerializeField]
	[Range(0f, 1f)]
	private float _maxXOffset = 1f;

	[Header("Random Spawn Rotation")]
	[SerializeField]
	private float _minRotation;

	[SerializeField]
	private float _maxRotation = 30f;

	[Header("Random Spawn Coin")]
	[SerializeField]
	[Range(0, 100)]
	private int _chanceSpawnCoin;

	[SerializeField]
	private float _yOffsetCoin = 0.5f;


	private float _fieldHalfWidth = 3.5f;
	private float _yPosition;

	private int _side = 1;

	private List<Basket> _baskets = new List<Basket>();

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
		_baskets.Clear();
		AddBasket(_startBasket);

		CreateNextBasket();
	}

	public void CheckLose(Ball ball)
	{
		if (_baskets.Count > 0)
		{
			if (_baskets[0].IsStart)
			{
				Respawn(ball);
			}
			else
			{
				OnLoseGame?.Invoke();
			}
		}
		
	}

	public float GetBottomLimitY()
	{
		if (_baskets.Count > 0)
		{
			return _baskets[0].transform.position.y - 2f;
		}

		return 0f;
	}

	public void SetHalfFieldWidth(float width)
	{
		_fieldHalfWidth = width;
	}

	public Vector2 GetCurrentBasketPosition()
	{
		return _baskets[0].transform.position;
	}

	[ContextMenu("NEW BASKET")]
	public void CreateNextBasket()
	{
		float width = (_fieldHalfWidth - 1f);
		float y = _yPosition + Random.Range(_minYOffset, _maxYOffset);
		float x = _side * Random.Range(_fieldHalfWidth * _minXOffset, _fieldHalfWidth * _maxXOffset);

		float zRotation = Random.Range(_minRotation * _side, _maxRotation * _side);
		Basket newBasket = Instantiate(_basketPrefab, new Vector2(x, y), Quaternion.Euler(0f, 0f, zRotation), transform);
		AddBasket(newBasket);

		TryCreateCoin(newBasket);

		_side *= -1;
	}

	private void TryCreateCoin(Basket basket)
	{
		int rand = Random.Range(0, 100);
		if (rand < _chanceSpawnCoin)
		{
			Vector2 position = basket.transform.position + basket.transform.up * _yOffsetCoin;
			Instantiate(_coinPrefab, position, Quaternion.identity);
		}
	}

	private void Respawn(Ball ball)
	{
		_baskets[0].SetDefaultAlignment();
		ball.SetPosition((Vector2)_baskets[0].transform.position + Vector2.up * _respawnYOffset);
	}

	private void AddBasket(Basket basket)
	{
		_baskets.Add(basket);
		_yPosition = basket.transform.position.y;

		CheckCountBaskets();
		SetLoseTrigger();
	}

	private void CheckCountBaskets()
	{
		if (_baskets.Count > _maxBaskets)
		{
			Basket oldBasket = _baskets[0];
			oldBasket.Destroy();
			_baskets.RemoveAt(0);
		}
	}

	private void SetLoseTrigger()
	{
		_loseTransform.position = _baskets[0].transform.position - Vector3.up * _loseYOffset;
	}
}
