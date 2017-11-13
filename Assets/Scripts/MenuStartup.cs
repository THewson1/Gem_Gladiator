using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStartup : MonoBehaviour {

    public GameObject gameObject;

    // Use this for initialization
    void Start () {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("MenuCanvas");
        foreach(GameObject go in gameObjectArray) {
            go.SetActive(false);
        }
        
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
