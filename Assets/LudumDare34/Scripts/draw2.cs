using UnityEngine;
using System.Collections;

public class draw2 : MonoBehaviour {

	public GameObject paint;
	public Transform treeParent;

	private Vector3 precPos;
	private TreeControl tc;
	
	// Use this for initialization
	void Start () {
		precPos = Vector3.up;
		tc = this.GetComponent<TreeControl>();
    }

	// Update is called once per frame
	void FixedUpdate()
	{
		GameObject g = Instantiate(paint) as GameObject;

		//g.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 4f);
		
		g.transform.position = tc.transform.position;
		Vector3 dir = g.transform.position - precPos;

		g.transform.rotation = tc.transform.rotation;
		precPos = g.transform.position;

		g.transform.parent = treeParent;

		float rot_z = Mathf.Atan2(tc.direction.y, tc.direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
