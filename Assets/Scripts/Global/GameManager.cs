using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	public event Action<bool> OnChangedPause;

	[SerializeField]
	private float _minImpulse = 5f;

	[SerializeField]
	private float _maxImpulse = 12f;

	[Space(10)]
	[SerializeField]
	private string _tagObstaclesShot;
	public string TagObstaclesShot { get { return _tagObstaclesShot; } }

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

		Application.targetFrameRate = 60;
	}

	private bool _canDrag;
	public bool CanDrag { get { return _canDrag; } }

	public void DisableDrag()
	{
		_canDrag = false;
	}

	public void EnebleDrag()
	{
		_canDrag = true;
	}

	public void SetPaused(bool isPaused)
	{
		OnChangedPause?.Invoke(isPaused);
	}

	public void Hit(int bounce, bool isClear)
	{
		_canDrag = true;
		LevelManager.instance.CreateNextBasket();
		ScoreManager.instance.AddScores(bounce, isClear);
	}

	public float CalculateImpulse(float distance)
	{
		float interpolant = Mathf.InverseLerp(0f, 0.35f, distance);
		float impulse = Mathf.Lerp(_minImpulse, _maxImpulse, interpolant);
		return impulse;
	}
}
