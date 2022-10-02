using UnityEngine;

[CreateAssetMenu(fileName = "BallsCollection", menuName = "Asset Settings/Balls Collection")]
public class BallsCollection : ScriptableObject
{
	public BallSetting[] Balls;
}
