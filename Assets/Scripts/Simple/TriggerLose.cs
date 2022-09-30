using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLose : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball)
			LevelManager.instance.CheckLose(ball);
	}
}
