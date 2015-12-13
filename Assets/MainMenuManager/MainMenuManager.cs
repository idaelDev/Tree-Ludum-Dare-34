using UnityEngine;
using System.Collections;

public class MainMenuManager : Singleton<MainMenuManager> {

    public CanvasGroup MainCanvasGroup;
    public CanvasGroup CreditsCanvasGroup;
    public CanvasGroup TutorialCanvasGroup;

    public int mainSceneId;

    public void OnStartGame()
    {
        HideAllCanvas();
        GameManager.Instance.StartGame();
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    public void OnCredits()
    {
        HideAllCanvas();
        ShowCanvasGroup(CreditsCanvasGroup, true);
    }

    public void OnTutorial()
    {
        HideAllCanvas();
        ShowCanvasGroup(TutorialCanvasGroup, true);
    }

    public void OnMain()
    {
        HideAllCanvas();
        ShowCanvasGroup(MainCanvasGroup, true);
    }

    void HideAllCanvas()
    {
        ShowCanvasGroup(MainCanvasGroup, false);
        ShowCanvasGroup(CreditsCanvasGroup, false);
        ShowCanvasGroup(TutorialCanvasGroup, false);
    }

    void ShowCanvasGroup(CanvasGroup c, bool show)
    {
        c.alpha = (show)? 1 : 0;
        c.interactable = show;
        c.blocksRaycasts = show;
    }
}