using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleVibration: MonoBehaviour
{
	private Toggle _toggle;

	private void Awake()
	{
		_toggle = GetComponent<Toggle>();
	}

	private void OnEnable()
	{
		if (VibrationManager.instance)
		{
			_toggle.isOn = VibrationManager.instance.CanVibrate();
		}
	}
}
