using UnityEngine;
using System.Collections;

public class TreeControl : MonoBehaviour {

	public Vector3 direction;
	public float angle = 0f;
	public float speed = 1f;
	public int numPlayer = 0;

	private KeyCode left, right;


	// Use this for initialization
	void Start()
	{
		if(numPlayer == 0) {
			left = KeyCode.Q;
			right = KeyCode.D;
        } else {
			left = KeyCode.LeftArrow;
			right = KeyCode.RightArrow;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(left))
		{
			if (angle + 0.01f < Mathf.PI/2.2f)
				angle += 0.005f;

		}
		else if (Input.GetKey(right))
		{
			if (angle - 0.01f >-Mathf.PI / 2.2f)
				angle -= 0.005f;
		}

		direction = new Vector3(
			Mathf.Cos(angle + Mathf.PI / 2),
			Mathf.Sin(angle + Mathf.PI / 2),
			0f);
		direction.Normalize();
		
		this.transform.position += direction*Time.deltaTime * speed;
	}
}