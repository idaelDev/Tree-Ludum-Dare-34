using UnityEngine;
using System.Collections;

public class MainMenuManager : Singleton<MainMenuManager>
{

	public CanvasGroup MainCanvasGroup;
	public CanvasGroup CreditsCanvasGroup;
	public CanvasGroup EndCanvasGroup;
	public GameObject deletePlayer;

	public int mainSceneId;

	public void OnStartGame( int nbPlayers = 2) {
		if(nbPlayers == 1)
		{
			GameManager.Instance.nbPlayers = 1;
			Destroy(deletePlayer);
        } else GameManager.Instance.nbPlayers = 2;


		HideAllCanvas();
		GameManager.Instance.StartGame();
	}

	public void OnQuitGame() {
		if(Application.isEditor)
			Debug.Log("quitGame");
		Application.Quit();
	}

	public void OnCredits() {
		HideAllCanvas();
		ShowCanvasGroup(CreditsCanvasGroup, true);
	}

	public void OnEnd() {
		HideAllCanvas();
		ShowCanvasGroup(EndCanvasGroup, true);
	}

	public void OnMain() {
		HideAllCanvas();
		ShowCanvasGroup(MainCanvasGroup, true);
	}

	void HideAllCanvas() {
		ShowCanvasGroup(MainCanvasGroup, false);
		ShowCanvasGroup(CreditsCanvasGroup, false);
		ShowCanvasGroup(EndCanvasGroup, false);
	}

	void ShowCanvasGroup(CanvasGroup c, bool show) {
		StartCoroutine(ShowCanvasGroupCoroutine(c, show));
	}

	IEnumerator ShowCanvasGroupCoroutine(CanvasGroup c, bool show) {
		float alphaFin = (show) ? 1 : 0;
		float alphaStart = c.alpha;
		float t = 0f;
		while (t < 1f)
		{
			c.alpha = Mathf.Lerp(alphaStart, alphaFin, t);
			t += 0.05f;
			yield return new WaitForSeconds(0.01f);
		}


		c.alpha = (show) ? 1 : 0;
		c.interactable = show;
		c.blocksRaycasts = show;
	}

}