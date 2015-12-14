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

	}

	void OnBecameInvisible()
	{
		BeatCount.BeatEvent -= OnBeat;
	}

	void OnBecameVisible()
	{
		BeatCount.BeatEvent += OnBeat;
	}

}

public enum PastilleType
{
    FLOWER,
    ANIMAL,
    FRUIT,
    BRANCH
}
