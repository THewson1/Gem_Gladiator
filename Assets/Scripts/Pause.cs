using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject pauseMenu;

    // Update is called once per frame
    void Update ()
    {
        foreach (InputDevice device in InputManager.Devices)
        {
            if (device.MenuWasPressed || Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale != 0)
                    PauseGame();
                else if (Time.timeScale == 0)
                    PlayGame();
            }   
        }
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayGame();
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
