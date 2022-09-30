using System;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
	public static TouchManager instance = null;

	public event Action<Vector2, float> OnSlide;
	public event Action<Vector2, float> OnRelease;
	public event Action OnTap;
 
	private Vector2 _startTap;
	private Vector2 _currentTap;

	private bool _onPointer;

	private Vector2 _vectorTap;
	private float _distanceTap;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		} 
		else if(instance == this)
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		if (!GameManager.instance.CanDrag)
			return;

		//Get Mouse / Touch Positions
		MouseListen();

		CalculateTap();

		if (Input.GetMouseButton(0))
			OnSlide?.Invoke(_vectorTap, _distanceTap);
	}

	public void PointerOn()
	{
		_onPointer = true;
	}

	public void PointerOff()
	{
		_onPointer = false;
	}

	private void CalculateTap()
	{
		_vectorTap = -(_currentTap - _startTap).normalized;
		_distanceTap = Vector2.Distance(_startTap, _currentTap);
		_distanceTap /= Screen.height;
	}

	private void MouseListen()
	{
		if (Input.GetMouseButtonDown(0) && _onPointer)
		{
			OnTap?.Invoke();
			_startTap = Input.mousePosition;
		}

		if (Input.GetMouseButton(0) && _onPointer && _startTap != Vector2.zero)
		{
			_currentTap = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0) && _startTap != Vector2.zero)
		{
			Release();
		}
	}

	private void Release()
	{
		_startTap = Vector2.zero;
		_currentTap = Vector2.zero;
		OnRelease?.Invoke(_vectorTap, _distanceTap);
	}
}
