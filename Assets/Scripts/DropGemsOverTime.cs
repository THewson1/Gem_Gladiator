﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGemsOverTime : MonoBehaviour {

    public GameObject m_gemPrefab;
    [Range(1f, 10)] public float m_waitTime = 1;
    private float m_timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;
        if (m_timer >= m_waitTime)
        {
            
            Instantiate(m_gemPrefab, transform.position, Quaternion.identity);
            m_timer = 0;
        }
	}
}
