using UnityEngine;
using System.Collections;

public class SoundManager :  Singleton<SoundManager>{

    BeatCount beatCounter;

    public AudioSource Menu;
    public AudioSource Close;
    public AudioSource Far;

    public float fadeTime = 0.5f;
    public float musicMaxVolume = 0.8f;

	private bool coroutineStarted = false;
    private AudioSource current;
	private bool gameEnded = false;

    void Start()
    {
		beatCounter = GetComponent<BeatCount>();
        current = Menu;
		End.EndEvent += EndGame;
	}

    public void StartSound()
    {
        Close.Play();
        Far.Play();
        SetSound(Close);
    }

    public void SetSound(AudioSource next)
    {
		if (!coroutineStarted && next != current && !gameEnded)
		{
			StartCoroutine(SetSound_Coroutine(current, next));
			current = next;
		}
    }

    IEnumerator SetSound_Coroutine(AudioSource old,AudioSource next)
    {
		coroutineStarted = true;
        float t=0;
        while(t<fadeTime)
        {
            old.volume = Mathf.Lerp(musicMaxVolume, 0, t / fadeTime);
            next.volume = Mathf.Lerp(0, musicMaxVolume, t / fadeTime);
            t += Time.deltaTime;
            yield return 0;
        }
        old.volume = 0.0f;
        next.volume = musicMaxVolume;

		coroutineStarted = false;
    }
    
	void EndGame() {
		SetSound(Far);
		gameEnded = true;
    }
}

