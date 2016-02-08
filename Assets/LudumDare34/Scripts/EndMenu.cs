using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class EndMenu : MonoBehaviour {

	// Use this for initialization
	public UnityEngine.UI.Text menuText;
	public UnityEngine.UI.Text screenshotText;
	public UnityEngine.UI.Text feedbackText;

	private bool endGame = false;
	private bool screenshotTaken = false;

	void Start() {
		End.EndEvent += EndGame;
	}

	private void EndGame() {
		End.EndEvent -= EndGame;

		endGame = true;
	}

	public void TakeScreenShot() {
		if(!screenshotTaken) {
			StartCoroutine( Screenshot());
			screenshotTaken=true;
		}
	}

	private IEnumerator Screenshot() {
		menuText.text = "";
		screenshotText.text="";

		yield return new WaitForEndOfFrame();

		string fileName = "Screenshot"+
					DateTime.Now.Year+
					DateTime.Now.Month+
					DateTime.Now.Day+
					DateTime.Now.Hour+
					DateTime.Now.Minute+
					DateTime.Now.Second+".png";

		Application.CaptureScreenshot(fileName, 2);


		yield return new WaitForEndOfFrame();
		menuText.text = "Menu";
		feedbackText.text="Screenshot saved in the game data folder!         ";
	}

	public void RestartMenu() {
		if(endGame)
			SceneManager.LoadScene(0);
	}
}
