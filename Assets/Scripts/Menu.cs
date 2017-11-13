using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public void LoadSceneByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

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

    private void NewMenu (string menuToActivate) {
        GameObject newMenu = GameObject.Find(menuToActivate);
        newMenu.SetActive(true);
    }

    public void ExitProgram() {
        Application.Quit();
    }


}
