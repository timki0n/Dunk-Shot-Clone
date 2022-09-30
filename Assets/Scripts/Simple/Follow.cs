using UnityEngine;

public class Follow : MonoBehaviour
{
	[SerializeField]
	private Transform _target;

	[SerializeField]
	private float _upOffset;

	private void Update()
	{
		transform.position = _target.position + (transform.up * _upOffset);
		transform.rotation = _target.rotation;
	}
}
