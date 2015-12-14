using UnityEngine;
using System.Collections;

public class Pastille : MonoBehaviour {

    public PastilleType pType;
	public BonusType bType;


	public delegate void PastilleGrab_delegate(PastilleType type);
    public event PastilleGrab_delegate PastilleGrabEvent;

	void Start()
	{
		BeatCount.BeatEvent += OnBeat;
	}

    public void Catched()
    {
        PastilleGrabEvent(pType);
		BeatCount.BeatEvent -= OnBeat;
		Destroy(gameObject);
    }

	public void OnBeat()
	{
		if (GetComponentInChildren<Animator>())
			GetComponentInChildren<Animator>().Play("Rotate");

	}

	/*void OnBecameInvisible()
	{
		BeatCount.BeatEvent -= OnBeat;
	}

	void OnBecameVisible()
	{
		BeatCount.BeatEvent += OnBeat;
	}*/

}

public enum PastilleType
{
    FLOWER,
    ANIMAL,
    FRUIT,
    BRANCH
}

public enum BonusType
{
	FAST,
	NORMAL,
	SLOW
}
