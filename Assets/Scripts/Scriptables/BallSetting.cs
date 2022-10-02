using UnityEngine;

[CreateAssetMenu(fileName = "BallSetting", menuName = "Asset Settings/Ball")]
public class BallSetting : ScriptableObject
{
	[Header("Visual")]
	public Sprite Sprite;

	[Header("Audio")]
	public AudioClip AudioTap;
	public float PitchAudioTap = 1f;

	[Header("Shop")]
	public int Price = 50;
}
