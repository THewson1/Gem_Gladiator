using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject m_pauseMenu;
    public AudioSource m_inGameMusic;
    public AudioSource m_pauseMenuMusic;

    // Update is called once per frame
    void Update ()
    {
        foreach (InputDevice device in InputManager.Devices)
        {
            if (device.MenuWasPressed)
            {
                pauseButtonWasPressed();
            }   
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            pauseButtonWasPressed();
        }
    }

    private void pauseButtonWasPressed()
    {
        if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<EndGameCondition>().m_gameOver)
        {
            if (Time.timeScale != 0)
            {
                m_inGameMusic.Pause();
                PauseGame();
                m_pauseMenuMusic.Play();
            }
            else if (Time.timeScale == 0)
            {
                m_pauseMenuMusic.Pause();
                PlayGame();
                m_inGameMusic.UnPause();
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
        m_pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        m_pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
