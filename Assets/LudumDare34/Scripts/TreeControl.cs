using UnityEngine;
using System.Collections;

public class TreeControl : MonoBehaviour {

	public Vector3 direction;
	public float angle = 0f;
	private float speed = 1f;
	public int numPlayer = 0;
    public float rotateSpeed = 8f;

	private KeyCode left, right;

    public Pastille currentpowerUp = null;
	public GameObject nodeObject;
	private GameObject currentNode;
	private ArrayList nodeList;
	public Transform treeParent;

    private const float ANGLE_OFFSET = 0.01f;
    private const float ANGLE_CONSTRAINT = 2.2f;

    private string playerHorizontal;
    private string playerFire;

	public float[] speedStates =  {0.8f, 1.1f, 1.5f};

	// Use this for initialization
	void Start()
	{
		speed = speedStates[1];
		currentNode = Instantiate(nodeObject) as GameObject;
		currentNode.transform.parent = treeParent;
		GetComponent<Draw>().node = currentNode.transform;

		nodeList = new ArrayList();
		nodeList.Add(currentNode);

        if (numPlayer == 0) {
            playerHorizontal = "Horizontal_p1";
            playerFire = "Fire_p1";
        } else {
            playerHorizontal = "Horizontal_p2";
            playerFire = "Fire_p2";
		}
	}

	// Update is called once per frame
	void Update()
	{
        if (GameManager.Instance.gameStarted)
        {
            float rotate = Input.GetAxisRaw(playerHorizontal);
            //..
            Rotation(rotate);

            if (Input.GetButtonDown(playerFire))
            {
                if (currentpowerUp != null)
                {
					this.speed = speedStates[(int)currentpowerUp.bType];

					currentpowerUp.Catched();
					GetComponent<Draw>().drawBranches(currentNode, currentpowerUp.pType);
					currentNode = Instantiate(nodeObject) as GameObject;
					currentNode.transform.parent = treeParent;
					GetComponent<Draw>().node = currentNode.transform;
					nodeList = new ArrayList();
					nodeList.Add(currentNode);
				}
            }
		}
	}

    public void Rotation(float rotate)
    {
        if (rotate<0)
        {
            if (angle + ANGLE_OFFSET < Mathf.PI / ANGLE_CONSTRAINT)
                angle += rotateSpeed * Time.deltaTime;

        }
        else if (rotate > 0)
        {
            if (angle - ANGLE_OFFSET > -Mathf.PI / ANGLE_CONSTRAINT)
                angle -= rotateSpeed * Time.deltaTime;
        }

        //..

        direction = new Vector3(
        Mathf.Cos(angle + Mathf.PI / 2),
        Mathf.Sin(angle + Mathf.PI / 2),
        0f);
        direction.Normalize();

        //..

        this.transform.position += direction * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "PowerUp")
        {
            currentpowerUp = other.gameObject.GetComponent<Pastille>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            currentpowerUp = null;
        }
    }
}