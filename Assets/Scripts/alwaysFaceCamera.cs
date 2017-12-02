using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour {

    Transform m_mainCamera;

	// Use this for initialization
	void Start () {
        m_mainCamera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = -m_mainCamera.transform.forward;
	}
}
