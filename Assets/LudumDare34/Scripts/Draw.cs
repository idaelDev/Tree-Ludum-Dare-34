using UnityEngine;
using System.Collections;
using System;

public class Draw : MonoBehaviour
{

	public GameObject[] paint;
	public Transform node { get; set; }

	public int distanceSpawnMin = 30;
	public int distanceSpawnMax = 40;

	private Vector3 precPos;
	private TreeControl treeControl;

	public float wait = 0.1f;
	private int cptBranches = 0;
	private int nextBranch = 0;

	public PastilleType feuillesType = PastilleType.SIMPLE;

	// Use this for initialization
	void Start()
	{
		End.EndEvent += EndGame;
		nextBranch = UnityEngine.Random.Range(distanceSpawnMin, distanceSpawnMax);
        if (node == null && GameManager.Instance.verbose)
			Debug.Log("Pas de parent pour les brushes!");
		precPos = Vector3.up;
		treeControl = GetComponent<TreeControl>();
		StartCoroutine(drawTreeParts());
	}

	void Update() {
		/*if(GameManager.Instance.gameStarted) {
			float rot_z = Mathf.Atan2(treeControl.direction.y, treeControl.direction.x)*Mathf.Rad2Deg;
			transform.rotation=Quaternion.Euler(0f, 0f, rot_z-90);
		}*/
	}

	IEnumerator drawTreeParts()
	{
		while (true)
		{
			if (GameManager.Instance.gameStarted)
			{
				int ind = UnityEngine.Random.Range(0, paint.Length);
				GameObject newTreePart = Instantiate(paint[ind]) as GameObject;

				//g.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 4f);

				newTreePart.transform.position = treeControl.transform.position + Vector3.right * UnityEngine.Random.value * 0.1f;
				Vector3 dir = newTreePart.transform.position - precPos;

				newTreePart.transform.rotation = treeControl.transform.rotation;
				precPos = newTreePart.transform.position;

				newTreePart.transform.parent = node;

				//float rot_z = Mathf.Atan2(treeControl.direction.y, treeControl.direction.x) * Mathf.Rad2Deg;
				//transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

				cptBranches++;
				if (cptBranches == nextBranch)
				{
					GetComponent<Draw>().drawBranches(node.gameObject, PastilleType.SIMPLE, false, feuillesType);
					nextBranch = UnityEngine.Random.Range(distanceSpawnMin, distanceSpawnMax);
					cptBranches = 0;
				}
			}
			yield return new WaitForSeconds(wait);
		}
	}

	public void drawBranches(GameObject currentNode, PastilleType numType, bool onBeat, PastilleType feuilleType = PastilleType.BRANCH)
	{
		StartCoroutine(drawBranchesOnNode(currentNode, numType, onBeat, feuilleType));
	}

	IEnumerator drawBranchesOnNode(GameObject node, PastilleType numType, bool onBeat, PastilleType feuilleType)
	{
		float waitTime = 0.25f;
		if (numType == PastilleType.SIMPLE)
			waitTime = 0.5f;

		yield return new WaitForSeconds(waitTime);
		GameObject branch;
        {
			GameObject[] branches = GameManager.Instance.getAssetsBranches(numType);
			branch = Instantiate(branches[UnityEngine.Random.Range(0, branches.Length)]) as GameObject;
			branch.transform.parent = node.transform.GetChild(node.transform.childCount - 1);
			branch.transform.localPosition = Vector3.zero;
			//branch.transform.localRotation = Quaternion.identity;

			UnityEngine.Random.seed *= (int)(54136841 * Time.timeSinceLevelLoad);

			float size;
			if (onBeat)
				size = UnityEngine.Random.Range(6f, 8.5f);
			else
				size = UnityEngine.Random.Range(3.5f, 6f);

			Vector3 newScale = new Vector3(size, size, 1);
			branch.transform.localScale = newScale;
		}
		
		if (feuilleType != PastilleType.BRANCH && feuilleType != PastilleType.SIMPLE) {

			waitTime = 0.25f;
			if (numType == PastilleType.SIMPLE)
				waitTime = 0.5f;
			yield return new WaitForSeconds(waitTime);


			GameObject[] feuilles = GameManager.Instance.getAssetsFeuilles(feuilleType);
			GameObject feuille = Instantiate(feuilles[UnityEngine.Random.Range(0, feuilles.Length)]) as GameObject;
			feuille.transform.parent = node.transform.GetChild(node.transform.childCount - 1);
			feuille.transform.localPosition = Vector3.zero;
			//feuille.transform.localRotation = ;

			UnityEngine.Random.seed *= (int)(54136841 * Time.timeSinceLevelLoad);

			Vector3 newScale = new Vector3(0.5f, 0.5f, 0.5f);
			feuille.transform.localScale = newScale;
		}

	}

	private void EndGame() {
		End.EndEvent -= EndGame;
		StopAllCoroutines();
	}

}
