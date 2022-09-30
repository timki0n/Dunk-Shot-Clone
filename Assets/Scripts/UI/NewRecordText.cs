using UnityEngine;

public class NewRecordText : MonoBehaviour
{
	private void Start()
	{
		ScoreManager.instance.OnNewRecord += OnNewRecord;
		gameObject.SetActive(false);
	}

	private void OnNewRecord()
	{
		gameObject.SetActive(true);
	}
}
