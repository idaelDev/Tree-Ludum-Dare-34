using UnityEngine;
using System.Collections;
using System;

public class Pastille : MonoBehaviour {

    public PastilleType pType;
	public BonusType bType;

	public delegate void PastilleGrab_delegate(PastilleType type);
    public event PastilleGrab_delegate PastilleGrabEvent;

	

	void Start()
	{
		BeatCount.BeatEvent += OnBeat;
		End.EndEvent += EndGame;
	}

    public void Catched()
    {
        SoundManager.Instance.GetComponents<AudioSource>()[(int)pType].Play();
        PastilleGrabEvent(pType);
		BeatCount.BeatEvent -= OnBeat;
		Destroy(gameObject);
    }

	public void OnBeat()
	{
		if (GetComponentInChildren<Animator>())
			GetComponentInChildren<Animator>().Play("Rotate");

	}

	void EndGame() {
		BeatCount.BeatEvent -= OnBeat;
		End.EndEvent -= EndGame;
		if(GameManager.Instance.verbose)
			Debug.Log("endPastille");
		try {
			Destroy(this.gameObject);
		} catch (Exception e) {
			return;
		}
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
    BRANCH, 
	SIMPLE
}

public enum BonusType
{
	FAST,
	NORMAL,
	SLOW,
	STOP
}
