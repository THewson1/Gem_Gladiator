using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Pause : MonoBehaviour {

    public GameObject pauseMenu;
    /*
        // Use this for initialization
        void Start () {

        }
   */

        // Update is called once per frame
    void Update ()
    {
        foreach (InputDevice device in InputManager.Devices)
        {
            if (device.Action4.WasPressed)
            {
                PauseGame();
            }
            
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
