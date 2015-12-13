using UnityEngine;
using System.Collections;

public class BeatCount : MonoBehaviour {

    public float t = 0.0f;
    public float deltaTime;
    public float bpm = 0f;
    public float beat = 0.0f;
    public float beatTot = 0.0f;
    static bool rhythmAble = false;
    public AudioSource Mo;
    private float expectedTime;
    private float timeOffset;

	public delegate void BeatDelegate();
	public static event BeatDelegate BeatEvent;

	// Use this for initialization
	void Start () {
        timeOffset = 60.0f / bpm;
        Debug.Log(timeOffset);
        expectedTime = timeOffset;
	}
	
	// Update is called once per frame
	void Update () {
	    t = Mo.time;

        if (t > expectedTime)
        {
			BeatEvent();
            expectedTime += timeOffset;
        }
	}
}
