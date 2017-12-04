using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    /// <summary>
    /// loads the specified scene and sets time to be normal
    /// </summary>
    public void LoadSceneByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    // Teagan's unused code
    public void LoadCanvasID (int canvasIndex) {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("MenuCanvas");
        foreach(GameObject go in gameObjectArray) {
            go.SetActive(false);
        }

        switch (canvasIndex) {
            case 0:
                NewMenu("MainMenu");
                break;

            case 1:
                NewMenu("ModeSelect");
                break;

            case 2:
                NewMenu("HowToPlay");
                break;

            case 3:
                NewMenu("Controls");
                break;

            case 4:
                NewMenu("PowerUps");
                break;

            case 5:
                NewMenu("Debuffs");
                break;
        }
    }

    // Teagan's unused code
    private void NewMenu (string menuToActivate) {
        GameObject newMenu = GameObject.Find(menuToActivate);
        newMenu.SetActive(true);
    }

    /// <summary>
    /// exit the game
    /// </summary>
    public void ExitProgram() {
        Application.Quit();
    }


}
