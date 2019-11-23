using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenuObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
                Pause();
            else
                Resume();
            isGamePaused = !isGamePaused;
        }
        
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuObj.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuObj.SetActive(false);
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu..");
        SceneManager.LoadScene(0); //0 is start menu
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
}
