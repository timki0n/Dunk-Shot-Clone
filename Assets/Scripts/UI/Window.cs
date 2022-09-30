using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
	[SerializeField]
	private Image _panel;

	[SerializeField]
	private Color _darkColor = Color.black;

	private Color _defaultColor;

	private void Awake()
	{
		if (_panel)
			_defaultColor = _panel.color;
	}

	private void OnEnable()
	{
		if (BackgroundManager.instance && _panel)
		{
			if (BackgroundManager.instance.IsDarkMode())
				_panel.color = _darkColor;
		}
	}

	private void Start()
	{
		if (BackgroundManager.instance && _panel)
			BackgroundManager.instance.OnChangedDarkMode += OnChangedDarkMode;
	}


	private void OnChangedDarkMode(bool isDark)
	{
		if (isDark)
			_panel.color = _darkColor;
		else
			_panel.color = _defaultColor;
	}

	public void Open()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
}
