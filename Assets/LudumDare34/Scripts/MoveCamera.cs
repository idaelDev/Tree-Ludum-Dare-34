using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
	//zoom max: zoomLevel = 0 (default)
	//medium: 1
	//min: 2

	public Transform p1, p2;

	private int[] camSize = { 4, 10, 20 };
	private int[] camPosMaxX = { 25, 13, 0 };

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
		End.EndEvent += EndGame;
        lerpPosValue = 0f;
		lerpZoomValue = 0f;
		StartCoroutine(changeCamera());
	}

	// Update is called once per frame

	void Update()
	{
		if (GameManager.Instance.nbPlayers == 2)
		{
			if (GameManager.Instance.gameStarted)
			{
				int newZoom = GetTargetZoomLevel();
				if (newZoom != targetZoom)
				{
					targetZoom = newZoom;
				}
				SetAudioZoom();
			}
		} else {
			if (p1 == null)
				p1 = p2;
			if (GameManager.Instance.gameStarted)
			{
				int newZoom = 1;
				if (newZoom != targetZoom)
				{
					targetZoom = newZoom;
				}
				SetAudioZoom();
			}
        }

		targetPos = new Vector3(
			PositionXInGamezone(currentZoom),
			PositionYInGamezone(currentZoom),//Mathf.Min(p1.position.y, p2.position.y),
			transform.parent.position.z);
	}


    void SetAudioZoom()
    {
        if(targetZoom == 2)
        {
            SoundManager.Instance.SetMusic(SoundManager.Instance.Far);
        }
        else
        {
            SoundManager.Instance.SetMusic(SoundManager.Instance.Close);
        }

		if (targetZoom == 0)
		{
			SoundManager.Instance.PlayNoise();
		} else
		{
			SoundManager.Instance.MuteNoise();
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
		else if (distance > 27) //Min
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

	public void EndGame() {
		End.EndEvent -= EndGame;
		StopAllCoroutines();

		StartCoroutine(cameraFin());
	}

	IEnumerator cameraFin() {
		float zoomFin = 150;
		float prevZoom = Camera.main.orthographicSize;

		Vector3 endPos = new Vector3(0, 130, 0);
		Vector3 prevPos = Camera.main.transform.position;

		Transform parentCamera = Camera.main.transform.parent;

		lerpZoomValue = 0f;
		lerpPosValue = 0f;
        while (lerpPosValue < 1f && lerpZoomValue < 1f)
		{
			if (lerpZoomValue < 1f)
			{
				lerpZoomValue += 0.005f;
				Camera.main.orthographicSize = Mathf.Lerp(prevZoom, zoomFin, lerpZoomValue);
			}
			if (lerpPosValue < 1f)
			{
				lerpPosValue += 0.003f;
				parentCamera.position = Vector3.Lerp(parentCamera.position, endPos, lerpPosValue);
			}
			
			yield return new WaitForSeconds(0.01f);
		}
	}
}