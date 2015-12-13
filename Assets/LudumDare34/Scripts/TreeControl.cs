using UnityEngine;
using System.Collections;

public class TreeControl : MonoBehaviour {

	public Vector3 direction;
	public float angle = 0f;
	public float speed = 1f;
	public int numPlayer = 0;
    public float rotateSpeed = 0.005f;

	private KeyCode left, right;
    public Pastille currentpowerUp = null;

    private const float ANGLE_OFFSET = 0.01f;
    private const float ANGLE_CONSTRAINT = 2.2f;

    private string playerHorizontal;

	// Use this for initialization
	void Start()
	{
		if(numPlayer == 0) {
            playerHorizontal = "Horizontal_p1";
        } else {
            playerHorizontal = "Horizontal_p2";
		}
	}

	// Update is called once per frame
	void Update()
	{
        float rotate = Input.GetAxisRaw(playerHorizontal);
        //..
        Rotation(rotate);
	}

    public void Rotation(float rotate)
    {
        if (rotate<0)
        {
            if (angle + ANGLE_OFFSET < Mathf.PI / ANGLE_CONSTRAINT)
                angle += rotateSpeed;

        }
        else if (rotate > 0)
        {
            if (angle - ANGLE_OFFSET > -Mathf.PI / ANGLE_CONSTRAINT)
                angle -= rotateSpeed;
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
            Debug.Log(currentpowerUp);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            currentpowerUp = null;
            Debug.Log(currentpowerUp);
        }
    }
}