using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
	//zoom max: zoomLevel = 0 (default)
	//medium: 1
	//min: 2

	public Transform p1, p2;
	public BoxCollider2D[] displayZones;


	private int[] camSize = { 4, 8, 16 };
	private int[] camPosMaxX = { 20, 10, 0 };

	private int oldZoom = 0;
	private int targetZoom = 0;
	private int currentZoom = 0;

	bool mustChangeZoom;

	private Vector3 oldPos;
	private Vector3 targetPos;
	private float lerpPosValue = 0f;
	private float lerpZoomValue = 0f;


	// Use this for initialization
	void Start()
	{
		lerpPosValue = 0f;
		lerpZoomValue = 0f;
		StartCoroutine(changeCamera());
	}

	// Update is called once per frame

	void Update()
	{
		if (GameManager.Instance.nbPlayers == 2)
		{
			int newZoom = GetTargetZoomLevel();
			if (newZoom != targetZoom)
			{
				targetZoom = newZoom;
			}
		}

		targetPos = new Vector3(
			PositionXInGamezone(currentZoom),
			PositionYInGamezone(currentZoom),//Mathf.Min(p1.position.y, p2.position.y),
			transform.parent.position.z);
	}




	IEnumerator changeCamera()
	{
		while (true)
		{
			if (oldZoom != currentZoom)
			{
				if (lerpZoomValue <= 0.99f)
				{
					lerpZoomValue += 0.01f;
					Camera.main.orthographicSize = Mathf.Lerp(camSize[oldZoom], camSize[currentZoom], lerpZoomValue);
				}
				else if (lerpZoomValue != 1f)
				{
					Camera.main.orthographicSize = Mathf.Lerp(camSize[oldZoom], camSize[currentZoom], lerpZoomValue);
					oldZoom = currentZoom;
					lerpZoomValue = 1f;
				}
			}
			else if (currentZoom != targetZoom)
			{
				currentZoom = targetZoom;
				lerpZoomValue = 0f;
				lerpPosValue = 0f;
            }

			if (lerpPosValue < 1f)
			{
				lerpPosValue += 0.005f;

			}
			transform.parent.position = Vector3.Lerp(transform.parent.position, targetPos, lerpPosValue);
			yield return new WaitForSeconds(0.01f);
		}
	}



	private int GetTargetZoomLevel()
	{
		int newTargetZoom = 0;
		float distance = Vector3.Distance(p1.position, p2.position);
		float distanceY = Mathf.Max(
			Mathf.Abs(p1.position.y - transform.position.y),
			Mathf.Abs(p2.position.y - transform.position.y));

		float distanceX = Mathf.Max(
			Mathf.Abs(p1.position.y - transform.position.y),
			Mathf.Abs(p2.position.y - transform.position.y));

		if (distance < 10)  //Max
			newTargetZoom = 0;
		else if (distance > 22) //Min
			newTargetZoom = 2;
		else newTargetZoom = 1;  //Medium

		if (distanceY > 3.5 && newTargetZoom == 0)
		{
			newTargetZoom = 1;
		}
		else if (distanceY > 7 && newTargetZoom == 1)
		{
			newTargetZoom++;
		}


		return newTargetZoom;
	}

	public Vector3 CenterOfVectors(Vector3[] vectors)
	{
		Vector3 sum = Vector3.zero;
		if (vectors == null || vectors.Length == 0)
		{
			return sum;
		}

		foreach (Vector3 vec in vectors)
		{
			sum += vec;
		}
		return (sum / vectors.Length);
	}

	public float PositionXInGamezone(int zoom)
	{
		float posX;
		if (GameManager.Instance.nbPlayers == 2)
			posX = CenterOfVectors(new Vector3[] { p1.position, p2.position }).x;
		else
			posX = p1.position.x;

		if (posX > 0)
		{
			return Mathf.Min(posX, camPosMaxX[zoom]);
		}
		else
		{
			return Mathf.Max(posX, -camPosMaxX[zoom]);
		}
	}

	public float PositionYInGamezone(int zoom)
	{
		float posY;
		if (GameManager.Instance.nbPlayers == 2)
			posY = CenterOfVectors(new Vector3[] { p1.position, p2.position }).y;
		else
			posY = p1.position.y;

		if (zoom == 2)
			posY -= 2;
		return posY;

	}
}