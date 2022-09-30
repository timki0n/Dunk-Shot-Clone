using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleDarkMode: MonoBehaviour
{
	private Toggle _toggle;

	private void Awake()
	{
		_toggle = GetComponent<Toggle>();
	}

	private void OnEnable()
	{
		if (BackgroundManager.instance)
		{
			_toggle.isOn = BackgroundManager.instance.IsDarkMode();
		}
	}
}
