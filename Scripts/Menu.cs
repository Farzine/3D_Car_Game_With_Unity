using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject pauseMenuUI;
    public GameObject playerUI;
    public GameObject aboutUI;
    public static bool GameIsStopped = false;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        aboutUI.SetActive(false);
        playerUI.SetActive(true); 
        Time.timeScale = 1f;
        GameIsStopped = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); 
        aboutUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsStopped = true;
        playerUI.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("scene_day");
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Garage");
        Time.timeScale = 1f;
    }

    public void About()
    {
        aboutUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
        playerUI.SetActive(false);
        pauseMenuUI.SetActive(false);

    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    
}
