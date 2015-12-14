using UnityEngine;
using System.Collections;
using System;

public class GameManager : Singleton<GameManager> {

    public bool gameStarted = false;
	public int nbPlayers = 2;

	bool canRestartGame = false;

	public GameObject[] flowers;
	public GameObject[] animals;
	public GameObject[] fruits;
	public GameObject[] branches;
	public GameObject[] simpleBranches;

	public void StartGame()
    {
		if (!gameStarted)
		{
			End.EndEvent += EndGame;
			gameStarted = true;
			SoundManager.Instance.StartSound();
		}
    }

	void Update(){
		if(canRestartGame) {
			if(Input.anyKey || Input.GetButtonDown("Fire_p2") || Input.GetButtonDown("Fire_p2"))
				Application.LoadLevel(0);
		}
	}


	public void EndGame() {
		StartCoroutine(returnToMenu());
    }

	private IEnumerator returnToMenu()
	{
		yield return new WaitForSeconds(4f);
		canRestartGame = true;
    }

	public GameObject[] getAssetsBranches(PastilleType type) {
		switch(type) {
			case PastilleType.FLOWER:	
				return flowers;
			case PastilleType.BRANCH:
				return branches;
			case PastilleType.FRUIT:
				return fruits;
			case PastilleType.ANIMAL:
				return animals;
			case PastilleType.SIMPLE:
				return simpleBranches;
			default:
				return null;
        }

	}
}
