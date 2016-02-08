using UnityEngine;
using System.Collections;
using System;

public class TreeControl : MonoBehaviour {

	public Vector3 direction;
	public float angle = 0f;
	private float speed = 1f;
	public int numPlayer = 0;
	public float rotateSpeed = 8f;

	public Pastille currentpowerUp = null;
	public GameObject nodeObject;
	private GameObject currentNode;
	private ArrayList nodeList;
	public Transform treeParent;

	public float[] speedStates = { 1.2f, 1.45f, 1.7f };

	public Sprite[] avatar = {null, null, null };

	private SpriteRenderer avatarRenderer;

	private const float ANGLE_OFFSET = 0.01f;
	private const float ANGLE_CONSTRAINT = 2.2f;

	private string playerHorizontal;
	private string playerFire;

	private Coroutine movePlayer;


	// Use this for initialization
	void Start() {

		avatarRenderer = GetComponentInChildren<SpriteRenderer>();

		End.EndEvent+=EndGame;
		speed=speedStates[1];
		currentNode=Instantiate(nodeObject) as GameObject;
		currentNode.transform.parent=treeParent;
		GetComponent<Draw>().node=currentNode.transform;

		nodeList=new ArrayList();
		nodeList.Add(currentNode);

		if(numPlayer==0) {
			playerHorizontal="Horizontal_p1";
			playerFire="Fire_p1";
		} else {
			playerHorizontal="Horizontal_p2";
			playerFire="Fire_p2";
		}
		movePlayer=StartCoroutine(MovePlayer());
	}

	// Update is called once per frame
	void Update() {

	}

	IEnumerator MovePlayer() {
		while(!GameManager.Instance.gameStarted) {
			yield return new WaitForEndOfFrame();
		}
		while(true) {

			float rotate = Input.GetAxis(playerHorizontal);
			//..
			Rotation(rotate);

			if(Input.GetButtonDown(playerFire)) {
				if(currentpowerUp!=null) {

					bool onBeat = BeatCount.Instance.isOnBeat();
					int numSpeed = (int) currentpowerUp.bType;
					speed=speedStates[numSpeed];
					Debug.Log(numSpeed + " " + avatar[numSpeed].name);
					if(avatar[numSpeed] != null)
						avatarRenderer.sprite = avatar[numSpeed];

					GetComponent<Draw>().drawBranches(currentNode, currentpowerUp.pType, onBeat);
					GetComponent<Draw>().feuillesType=currentpowerUp.pType;


					currentpowerUp.Catched();
					currentNode=Instantiate(nodeObject) as GameObject;
					currentNode.transform.parent=treeParent;
					GetComponent<Draw>().node=currentNode.transform;
					nodeList=new ArrayList();
					nodeList.Add(currentNode);


					//feedback sonore
					GetComponent<AudioSource>().volume=0.35f;
					if(onBeat) {
						GetComponents<AudioSource>()[0].clip=Instantiate(Resources.Load("bonus_hit")) as AudioClip;
					} else {
						GetComponents<AudioSource>()[0].clip=Instantiate(Resources.Load("bonus_miss")) as AudioClip;
					}
					GetComponent<AudioSource>().Play();
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}

	public void Rotation(float rotate) {
		if(rotate<0) {
			if(angle+ANGLE_OFFSET<Mathf.PI/ANGLE_CONSTRAINT)
				angle+=rotateSpeed*Time.deltaTime;

		} else if(rotate>0) {
			if(angle-ANGLE_OFFSET>-Mathf.PI/ANGLE_CONSTRAINT)
				angle-=rotateSpeed*Time.deltaTime;
		}

		//..

		direction=new Vector3(
			Mathf.Cos(angle+Mathf.PI/2),
			Mathf.Sin(angle+Mathf.PI/2),
			0f);

		direction.Normalize();

		//..

		this.transform.position+=direction*Time.deltaTime*speed;

		//..

		float rot_z = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
		transform.rotation=Quaternion.Euler(0f, 0f, rot_z-90);

	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag=="PowerUp") {
			currentpowerUp=other.gameObject.GetComponent<Pastille>();
		} else if(other.gameObject.tag=="Fin") {
			End.TriggerEndGame();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag=="PowerUp") {
			currentpowerUp=null;
		}
	}

	private void EndGame() {
		this.speed=0f;
	}
}