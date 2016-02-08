using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public bool gameStarted = false;
	public int nbPlayers = 2;

	bool canRestartGame = false;

	public GameObject[] flowers;
	public GameObject[] animals;
	public GameObject[] fruits;
	public GameObject[] branches;
	public GameObject[] simpleBranches;

	public GameObject[] flowersFeuilles;
	public GameObject[] animalsFeuilles;
	public GameObject[] fruitsFeuilles;

	public void Awake() {
		
	}

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

			/*if(Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire_p2") || Input.GetButtonDown("Fire_p2"))
				SceneManager.LoadScene(0, LoadSceneMode.Additive);*/
		}
	}


	public void EndGame() {
		End.EndEvent -= EndGame;
		StartCoroutine(returnToMenu());
    }

	private IEnumerator returnToMenu()
	{
		yield return new WaitForSeconds(4f);
		canRestartGame = true;
		FindObjectOfType<MainMenuManager>().OnEnd();
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

	internal GameObject[] getAssetsFeuilles(PastilleType feuilleType) {
		switch (feuilleType)
		{
			case PastilleType.FLOWER:
				return flowersFeuilles;
			case PastilleType.FRUIT:
				return fruitsFeuilles;
			case PastilleType.ANIMAL:
				return animalsFeuilles;
			default:
				return null;
		}
	}
}
