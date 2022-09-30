using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleSound : MonoBehaviour
{
	private Toggle _toggle;

	private void Awake()
	{
		_toggle = GetComponent<Toggle>();
	}

	private void OnEnable()
	{
		if (AudioManager.instance)
		{
			_toggle.isOn = AudioManager.instance.IsSound();
		}
	}
}
