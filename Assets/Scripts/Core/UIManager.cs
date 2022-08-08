 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameEndScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        gameEndScreen.SetActive(false);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        // Application.Quit();                 <------ for build
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
