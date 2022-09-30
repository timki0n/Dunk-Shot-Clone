using UnityEngine;

public class Coin : MonoBehaviour
{
	private void Collect()
	{
		MoneyManager.instance.AddMoney();
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Ball ball = collision.GetComponent<Ball>();
		if (ball)
			Collect();
	}
}
