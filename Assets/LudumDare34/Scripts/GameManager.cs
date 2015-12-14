using UnityEngine;
using System.Collections;
using System;

public class GameManager : Singleton<GameManager> {

    public bool gameStarted = false;
	public int nbPlayers = 2;

	bool canRestartGame = false;

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
			if(Input.anyKey)
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
}
