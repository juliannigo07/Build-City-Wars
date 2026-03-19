using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void OnCreditsClicked()
    {
        ScreenManager.Instance.Show("CreditsPanel");
    }

    public void OnBackFromCredits()
    {
        ScreenManager.Instance.Show("MainMenuPanel");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
