﻿using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

    public bool gameStarted = false;
	public int nbPlayers = 2;

    public void StartGame()
    {
        gameStarted = true;
        SoundManager.Instance.StartSound();
    }
}
