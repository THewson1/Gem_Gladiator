using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alwaysFaceCamera : MonoBehaviour {

    Transform mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = -mainCamera.transform.forward;
	}
}
