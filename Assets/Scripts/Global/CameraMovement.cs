using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private float _offsetYFollow;

	[SerializeField]
	private float _rangeFollow = 3f;

	private Transform _ballTransform;

	private float _targetY;

	private void Awake()
	{
		_ballTransform = FindObjectOfType<Ball>().transform;
	}


	private void LateUpdate()
	{
		float ballY = _ballTransform.position.y + _offsetYFollow;
		float cameraY = transform.position.y ;
		float dist = cameraY - ballY;
		float adsDist = Mathf.Abs(dist);

		if (adsDist > _rangeFollow)
		{
			_targetY -= dist * Time.deltaTime;
			_targetY = Mathf.Clamp(_targetY, LevelManager.instance.GetBottomLimitY(), Mathf.Infinity);
		}

		float y = Mathf.Lerp(cameraY, _targetY, Time.deltaTime * 5);

		transform.position = new Vector3(0f, y, -10f);

	}
}
