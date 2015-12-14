using UnityEngine;
using System.Collections;
using System;

public class Draw : MonoBehaviour
{

	public GameObject[] paint;
	public Transform node { get; set; }

	private Vector3 precPos;
	private TreeControl tc;

	public float wait = 0.1f;

	// Use this for initialization
	void Start()
	{
		if (node == null)
			Debug.Log("Pas de parent pour les brushes!");
		precPos = Vector3.up;
		tc = GetComponent<TreeControl>();
		StartCoroutine(drawTree());
	}


	IEnumerator drawTree()
	{
		while (true)
		{
			if (GameManager.Instance.gameStarted)
			{
				//ToDo Coroutine? 
				int ind = UnityEngine.Random.Range(0, paint.Length);
				GameObject g = Instantiate(paint[ind]) as GameObject;

				//g.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 4f);

				g.transform.position = tc.transform.position + Vector3.right * UnityEngine.Random.value * 0.1f;
				Vector3 dir = g.transform.position - precPos;

				g.transform.rotation = tc.transform.rotation;
				precPos = g.transform.position;

				g.transform.parent = node;

				float rot_z = Mathf.Atan2(tc.direction.y, tc.direction.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			}
			yield return new WaitForSeconds(wait);
		}
	}

	public void drawBranches(GameObject currentNode, PastilleType numType)
	{
		StartCoroutine(drawBranchesOnNode(currentNode, numType));
	}

	IEnumerator drawBranchesOnNode(GameObject node, PastilleType numType)
	{
		int nbSkippedMax = 200;
		int nbSkippedMin = 100;
		int nb = 0;// UnityEngine.Random.Range(nbSkippedMin, nbSkippedMax);
		int chooseSide = 0;
        for (int i = node.transform.childCount-1; i > 15 ; i-=nb)
		{
			GameObject branch = Instantiate( Resources.Load("branch" + (int)numType)) as GameObject;
			branch.transform.parent = node.transform.GetChild(i);
			branch.transform.localPosition = Vector3.zero;
			branch.transform.localRotation = Quaternion.identity;

			UnityEngine.Random.seed *= 54136841;

			chooseSide = (chooseSide + UnityEngine.Random.Range(0, 4))%4;

			float size = UnityEngine.Random.Range(2.5f, 6f);

			Vector3 newScale;
			chooseSide = 0;
            switch (chooseSide)
			{
				case 0:
					newScale = new Vector3(size, size, 1);
					break;
				case 1:
					newScale = new Vector3(size, -size, 1);
					break;
				case 2:
					newScale = new Vector3(-size, size, 1);
					break;
				default:
					newScale = new Vector3(-size, -size, 1);
					break;
			}
			branch.transform.localScale = newScale;

			yield return new WaitForSeconds(0.25f);
			nb = UnityEngine.Random.Range(nbSkippedMin, nbSkippedMax);
		}

	}

}
