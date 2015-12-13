using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

    public bool gameStarted = false;    
    
    void Start()
    {
        gameStarted = true;
    }
}
