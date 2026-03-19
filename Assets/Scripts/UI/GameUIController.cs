using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUIController : MonoBehaviour
{
    public void OnResumeClicked()
    {
        ScreenManager.Instance.Show(""); 
        Time.timeScale = 1f;
    }

    public void OnBackToMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        ScreenManager.Instance.Show("PausePanel");
        Time.timeScale = 0f;
    }
}
