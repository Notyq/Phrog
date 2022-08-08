 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private GameObject gameStartScreen;

    private UIManager uiManager;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        gameEndScreen.SetActive(false);
        gameStartScreen.SetActive(true);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void GameEnd()
    {
        gameEndScreen.SetActive(true);
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

    public void StartGame()
    {
        SceneManager.LoadScene(0);
        uiManager.gameStartScreen.SetActive(false);
    }
}
