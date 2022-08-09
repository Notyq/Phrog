 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private GameObject gamePauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        gameEndScreen.SetActive(false);
        gamePauseScreen.SetActive(false);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void GameEnd()
    {
        gameEndScreen.SetActive(true);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        gamePauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gamePauseScreen.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        // Application.Quit();                 <------ for build
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
