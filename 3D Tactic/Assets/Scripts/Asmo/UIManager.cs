using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public static bool gameComplete;

    void Start()
    {
        gameComplete = false;
    }

    private void Update()
    {
        if (gameComplete)
        {
            SceneManager.LoadScene("IndoorScene");
        }
    }



    //Pelin aloitus
    public void PlayGame()
    {
        SceneManager.LoadScene("IndoorScene");
    }

    //Pelin sulkeminen
    public void QuitGame()
    {
        Application.Quit();
    }

    //Pause valikko, peli pys‰htyy
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    //Pause valikosta poistuminen, peli jatkuu
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    //Takaisin p‰‰valikkoon
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainScreen");
    }

    //Pelin uudelleen aloitus
    public void RestartGame()
    {

        SceneManager.LoadScene("OutdoorScene");
    }
}
