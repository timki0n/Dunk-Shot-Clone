using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallGhost: MonoBehaviour
{
	private Rigidbody2D _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public void Shot(Vector2 vector, float impulse)
	{
		_rigidbody.angularVelocity = 240f;
		_rigidbody.AddForce(vector * impulse, ForceMode2D.Impulse);
	}
}
