using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowsController : MonoBehaviour
{
	[SerializeField]
	private string _url;

	[Space(10)]
	[SerializeField]
	private Window _windowLose;

	[SerializeField]
	private GameObject _pauseButtonObject;


	private void Start()
	{
		LevelManager.instance.OnLoseGame += OnLoseGame;
		TouchManager.instance.OnTap += OnGameTap;
	}

	private void OnGameTap()
	{
		_pauseButtonObject.SetActive(true);
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnLoseGame()
	{
		_windowLose.Open();
		_pauseButtonObject.SetActive(false);
	}

	public void OnChangeSound(bool isSound)
	{
		AudioManager.instance.SetSound(isSound);
	}


	public void OnChangeVibration(bool isVibrate)
	{
		VibrationManager.instance.SetCanVibrate(isVibrate);
	}

	public void SetPaused(bool isPaused)
	{
		GameManager.instance.SetPaused(isPaused);
	}

	
	public void OnChangeDarkMode(bool isDark)
	{
		BackgroundManager.instance.SetDarkMode(isDark);
	}

	public void OpenUrl()
	{
		Application.OpenURL(_url);
	}
}
