using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject gamePlayPanel;
    public GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        gamePlayPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        if (isPaused) return;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (!isPaused) return;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
