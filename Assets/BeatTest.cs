using UnityEngine;
using System.Collections;

public class BeatTest : MonoBehaviour {

    public float t = 0.0f;
    public float deltaTime;
    public float bpm = 0f;
    public float beat = 0.0f;
    public float beatTot = 0.0f;
    public AudioSource Mo;
    private float expectedTime;
    private float timeOffset;
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
            Debug.Log("Beat");
            expectedTime += timeOffset;
        }
	}
}
