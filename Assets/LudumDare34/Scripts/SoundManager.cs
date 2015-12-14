using UnityEngine;
using System.Collections;

public class SoundManager :  Singleton<SoundManager>{

    BeatCount beatCounter;

    public AudioSource Menu;
    public AudioSource Close;
	public AudioSource Far;
	public AudioSource Growing;
	public AudioSource feedBack;

	public AudioSource[] Branches;

	public float fadeTime = 0.5f;
    public float musicMaxVolume = 0.8f;

	private bool setSound_Started = false;
	private bool noiseStarted = false;
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
        SetMusic(Close);
		PlayNoise();
    }

    public void SetMusic(AudioSource next)
    {
		if (!setSound_Started && next != current && !gameEnded)
		{
			StartCoroutine(SetSound_Coroutine(current, next));
			current = next;
		}
    }

	IEnumerator SetSound_Coroutine(AudioSource old,AudioSource next)
    {
		setSound_Started = true;
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

		setSound_Started = false;
    }

	public void PlayNoise() {
		if (!noiseStarted)
			StartCoroutine(volumeNoise(1f, 2f));
	}

	public void MuteNoise() {
		if(noiseStarted)
			StartCoroutine(volumeNoise(0f, 2f));
	}

	IEnumerator volumeNoise(float newVolume, float fade) {
		if (newVolume == 1f)
			noiseStarted = true;
		else noiseStarted = false;
		float oldVolume = Growing.volume;

        float t = 0;

		while (t < fade)
		{
			Growing.volume = Mathf.Lerp(oldVolume, newVolume, t);
			t += Time.deltaTime;
			yield return 0;
		}
		Growing.volume = newVolume;
	}

	void PlayBranchSound(int num) {
		//TODO bouger les audiosources
		if(!Branches[num].isPlaying)
		 {
			Branches[num].Play();
         }
	}

	void EndGame() {
		SetMusic(Far);
		gameEnded = true;
    }
}

