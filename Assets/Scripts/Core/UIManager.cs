 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private GameObject gamePauseScreen;
    [SerializeField] private GameObject gameStartScreen;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
        gameEndScreen.SetActive(false);
        gamePauseScreen.SetActive(false);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
    public void GameEnd()
    {
        gameEndScreen.SetActive(true);
        Time.timeScale = 0;
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

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        gameStartScreen.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        // Application.Quit();                 <------ for build
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
