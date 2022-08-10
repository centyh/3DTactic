using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;



    void Start()
    {

    }

   
    public void PlayGame()
    {
        SceneManager.LoadScene("IndoorScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void RestartGame()
    {

        SceneManager.LoadScene("OutdoorScene");
    }
}
