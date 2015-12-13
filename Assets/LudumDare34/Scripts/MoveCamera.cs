using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
	//zoom max: zoomLevel = 0 (default)
	//medium: 1
	//min: 2

	public Transform p1, p2;
	public BoxCollider2D[] displayZones;

	private int zoomLevel = 0;
	private int[] camSize = { 4, 8, 16 };
	private int[] camPosMaxX = { 20, 10, 0 };

	private Coroutine lerpCoroutine;
	private bool coroutineRunning = false;


	private int oldZoom = 0;
	private int targetZoom = 0;
	private Vector3 oldPos;
	private Vector3 targetPos;
	private float lerpPosValue = 0f;
	private float lerpZoomValue = 0f;
	

	// Use this for initialization
	void Start()
	{
		lerpCoroutine = StartCoroutine(changeZoom());
	}

	// Update is called once per frame
	void Update()
	{
		int newZoom = GetTargetZoomLevel();
		if (newZoom != targetZoom)
		{
			lerpPosValue = 0f;
			lerpZoomValue = 0f;
			oldZoom = targetZoom;
			targetZoom = newZoom;

			
		}

		targetPos = new Vector3(
			PositionXInGamezone(targetZoom),
			PositionYInGamezone(targetZoom),//Mathf.Min(p1.position.y, p2.position.y),
			transform.parent.position.z);

		if (!coroutineRunning)
		{
			lerpCoroutine = StartCoroutine(changeZoom());
		}
	}


		

	IEnumerator changeZoom() {
		coroutineRunning = true;
		while (true)
		{
			if (lerpZoomValue < 0.99f)
				lerpZoomValue += 0.01f;
			else lerpZoomValue = 1f;

			if (lerpPosValue < 0.995f)
				lerpPosValue += 0.005f;
			else lerpPosValue = 1f;
			transform.parent.position = Vector3.Lerp(transform.parent.position, targetPos, lerpPosValue);
			Camera.main.orthographicSize = Mathf.Lerp(camSize[oldZoom], camSize[targetZoom], lerpZoomValue);
			yield return new WaitForSeconds(0.01f);
		}

		//coroutineRunning = false;
    }

	private int GetTargetZoomLevel()
	{
		int newTargetZoom = 0;
		float distance = Vector3.Distance(p1.position, p2.position);
		Debug.Log(distance);
		if (distance < 12)
			Camera.main.orthographicSize = 4;
		else if (distance > 22)
			Camera.main.orthographicSize = 16;
		else Camera.main.orthographicSize = 8;
		this.transform.parent.position = new Vector3(
			this.transform.parent.position.x,
			Mathf.Min(p1.position.y, p2.position.y),
			this.transform.parent.position.z);

		Debug.Log(CenterOfVectors(new Vector3[] { p1.position, p2.position}));
			newTargetZoom = 0;
		else if (distance > 22) //Min
			newTargetZoom = 2;
		else newTargetZoom = 1;  //Medium



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
		float posX = CenterOfVectors(new Vector3[] { p1.position, p2.position }).x;
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
		float posY = CenterOfVectors(new Vector3[] { p1.position, p2.position }).y;
		if (zoom == 2)
			posY -= 2;
		return posY;

	}	
}