using UnityEngine;

[CreateAssetMenu(fileName = "BasketSetting", menuName = "Asset Settings/Basket")]
public class BasketSetting : ScriptableObject
{
	public float TimeSleepAfterShot = 0.3f;

	[Space(10)]

	public float MinStretch = 1f;
	public float MaxStretch = 1.85f;

	[Space(10)]

	public AnimationCurve AlignmentCurve;
	public float TimeAlignment = 0.2f;

	[Space(10)]

	public AnimationCurve ScaleCurve;
	public float TimeScale = 0.2f;

	[Space(10)]

	public AnimationCurve NetShakeCurve;
	public float TimeShakeNet = 0.1f;
}
