using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	[SerializeField]
	private AudioSource _tapSound;

	[SerializeField]
	private AudioSource _whipSound;

	private Rigidbody2D _rigidbody;

	private Basket _prevBasket;

	private bool _isCleanShot;

	private int _bounce = 1;

	private Vector2 _saveVelocity;
	private float _saveAngularVelocity;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		GameManager.instance.OnChangedPause += OnChangedPause;
	}

	public void SetInBasket(Transform parent, Basket basket)
	{
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.simulated = false;
		transform.SetParent(parent);
		transform.localPosition = Vector2.zero;
		CheckBasket(basket);
	}

	public void Shot(Vector2 vector, float impulse)
	{
		GameManager.instance.DisableDrag();
		_isCleanShot = true;
		_bounce = 1;
		transform.SetParent(transform.parent.parent);
		_rigidbody.angularVelocity = 240f;
		_rigidbody.simulated = true;
		_rigidbody.AddForce(vector * impulse, ForceMode2D.Impulse);
		_whipSound.Play();
	}

	public void SetPosition(Vector2 position)
	{
		transform.position = position;
		_rigidbody.velocity = Vector2.zero;
	}

	private void CheckBasket(Basket basket)
	{
		if (!basket.IsStart && basket != _prevBasket)
		{
			GameManager.instance.Hit(_bounce, _isCleanShot);
		}
		else
			GameManager.instance.EnebleDrag();

		_prevBasket = basket;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		_tapSound.Play();

		Basket basket = collision.gameObject.GetComponent<Basket>();

		if (basket)
		{
			_isCleanShot = false;
			return;
		}

		ScreenCollider screen = collision.gameObject.GetComponent<ScreenCollider>();
		if (screen)
		{
			_bounce += 1;
		}
	}

	private void OnChangedPause(bool isPaused)
	{
		if (isPaused)
		{
			_saveAngularVelocity = _rigidbody.angularVelocity;
			_saveVelocity = _rigidbody.velocity;
			_rigidbody.velocity = Vector2.zero;
			_rigidbody.angularVelocity = 0f;
			_rigidbody.isKinematic = true;
		}

		else
		{
			_rigidbody.angularVelocity = _saveAngularVelocity;
			_rigidbody.isKinematic = false;
			_rigidbody.velocity = _saveVelocity;
		}
	}
}
