using System.Collections;
using UnityEngine;

public class Basket : MonoBehaviour
{
	[SerializeField]
	private BasketSetting _setting;

	public bool IsStart;

	[SerializeField]
	private AudioSource _sound;

	[SerializeField]
	private Net _net;

	[SerializeField]
	private Follow _ballFollow;

	[SerializeField]
	private Transform _visualTransform;

	[SerializeField]
	private Ball _ball;

	private Rigidbody2D _rigidbody;

	private BoxCollider2D _trigger;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_trigger = GetComponent<BoxCollider2D>();
	}

	private void Start()
	{
		AddListeners();
		CheckBall();

		transform.localScale = Vector3.zero;
		StartCoroutine(AnimateScaleTo(1));
	}

	private void AddListeners()
	{
		TouchManager touchController = TouchManager.instance;
		if (touchController)
		{
			touchController.OnSlide += StretchAndRotate;
			touchController.OnRelease += Release;
		}
	}

	public void Destroy()
	{
		StopAllCoroutines();
		StartCoroutine(AnimateScaleTo(0));
		DisableTriggers();

		if (!IsStart)
			Destroy(gameObject, _setting.TimeScale);
	}

	public void SetDefaultAlignment()
	{
		StartCoroutine(AnimateAlignment(Quaternion.identity));
	}

	private void Release(Vector2 vector, float impulse)
	{
		if (!_ball) return;
		_net.Shake();

		if (impulse < 0.08f) return;

		DisableTriggers();
		_ball.Shot(vector, GameManager.instance.CalculateImpulse(impulse));
		_ball = null;
		Invoke(nameof(ActivateTriggers), _setting.TimeSleepAfterShot);
	}

	private void StretchAndRotate(Vector2 vector, float distance)
	{
		if (!_ball) return;

		StopAllCoroutines();

		float interpolant = Mathf.InverseLerp(0f, 0.4f, distance);
		float yScale = Mathf.Lerp(_setting.MinStretch, _setting.MaxStretch, interpolant);

		_visualTransform.localScale = new Vector2(1f, yScale);

		transform.rotation = Quaternion.LookRotation(Vector3.forward, vector);
	}

	private void CheckBall()
	{
		if (!_ball) return;
		_trigger.enabled = false;
		_ball.SetInBasket(_ballFollow.transform, this);
	}

	private void SetBall(Ball ball)
	{
		_sound.Play();
		_ball = ball;
		CheckBall();
		_net.Shake();
		SetDefaultAlignment();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Ball ball = collision.GetComponent<Ball>();
		if (ball)
		{
			SetBall(ball);
		}
	}

	private void DisableTriggers()
	{
		_rigidbody.simulated = false;
		_trigger.enabled = false;
	}

	private void ActivateTriggers()
	{
		_rigidbody.simulated = true;
		_trigger.enabled = true;
	}

	IEnumerator AnimateAlignment(Quaternion toQ)
	{
		Quaternion startQ = transform.rotation;
		float sec = _setting.TimeAlignment;
		for (float t = 0; t < 1; t+=Time.deltaTime / sec)
		{
			transform.rotation = Quaternion.Lerp(startQ, toQ, _setting.AlignmentCurve.Evaluate(t));
			yield return null;
		}
		transform.rotation = toQ;
	}

	IEnumerator AnimateScaleTo(float amount)
	{
		Vector3 startScale = transform.localScale;
		float sec = _setting.TimeScale;
		for (float t = 0; t < 1; t+=Time.deltaTime / sec)
		{
			transform.localScale = Vector3.Lerp(startScale, Vector3.one * amount, _setting.ScaleCurve.Evaluate(t));
			yield return null;
		}
		transform.localScale = Vector3.one * amount;
	}
}
