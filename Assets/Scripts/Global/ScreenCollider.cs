using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
	private void Start()
	{
		GenerateColliders();
	}

	void GenerateColliders()
	{
		Plane[] planeLeftRight = GetLeftRightFrusrtums();
		for (int i = 0; i < planeLeftRight.Length; i++)
		{
			AddCollider(planeLeftRight[i].ClosestPointOnPlane(transform.position).x);
		}

		LevelManager.instance.SetHalfFieldWidth(planeLeftRight[1].ClosestPointOnPlane(transform.position).x);
	}

	void AddCollider(float xOffset)
	{
		BoxCollider2D boxC = gameObject.AddComponent<BoxCollider2D>();
		boxC.size = new Vector2(Mathf.Abs(xOffset * 2f), 50f);
		boxC.offset = new Vector2(xOffset * 2f, 0f);
	}

	Plane[] GetLeftRightFrusrtums()
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		Plane[] planeLeftRight = new Plane[2];
		planeLeftRight[0] = planes[0];
		planeLeftRight[1] = planes[1];
		return planeLeftRight;
	}
}
