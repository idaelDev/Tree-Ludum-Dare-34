using UnityEngine;
using System.Collections;

public class Draw : MonoBehaviour {

	public GameObject[] paint;
	public Transform treeParent;

	private Vector3 precPos;
	private TreeControl tc;

    public float wait = 0.1f;
	
	// Use this for initialization
	void Start () {
		if (treeParent == null)
			Debug.Log("Pas de parent pour les brushes!");
		precPos = Vector3.up;
		tc = GetComponent<TreeControl>();
		StartCoroutine(drawTree());
    }


	IEnumerator drawTree()
	{
		while (true)
		{
			//ToDo Coroutine? 
            int ind = Random.Range(0, paint.Length);
			GameObject g = Instantiate(paint[ind]) as GameObject;

			//g.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 4f);

			g.transform.position = tc.transform.position;
			Vector3 dir = g.transform.position - precPos;

			g.transform.rotation = tc.transform.rotation;
			precPos = g.transform.position;

			g.transform.parent = treeParent;

			float rot_z = Mathf.Atan2(tc.direction.y, tc.direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

            yield return new WaitForSeconds(wait);
		}
	}
	void FixedUpdate()
	{
		
    }
}
