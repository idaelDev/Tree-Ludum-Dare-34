using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public Transform p1, p2;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
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
		return sum / vectors.Length;
	}
}