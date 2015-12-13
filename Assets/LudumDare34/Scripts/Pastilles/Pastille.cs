using UnityEngine;
using System.Collections;

public class Pastille : MonoBehaviour {

    public PastilleType type;

    public delegate void PastilleGrab_delegate(PastilleType type);
    public event PastilleGrab_delegate PastilleGrabEvent;

	void Start()
	{
		
	}

    public void Catched()
    {
        PastilleGrabEvent(type);
        Destroy(gameObject);
    }

	public void OnBeat()
	{
		if (this.GetComponentInChildren<Animator>())
			GetComponentInChildren<Animator>().Play("Rotate");
		Debug.Log("bbbbeat");

	}

	void OnBecameInvisible()
	{
		BeatCount.BeatEvent -= this.OnBeat;
		Debug.Log("invisible");
	}

	void OnBecameVisible()
	{
		BeatCount.BeatEvent += OnBeat;
		Debug.Log("visible");
	}

}

public enum PastilleType
{
    FLOWER,
    ANIMAL,
    FRUIT,
    BRANCH
}
