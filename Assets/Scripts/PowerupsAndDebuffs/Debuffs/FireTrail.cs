using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrail : PowerupOrDebuff {

    private GameObject[] m_boulders;
    private float m_timer = 0;

    public GameObject m_fire;
    [Tooltip("How often to spawn fire in secconds")]
    [Range(0, 1)] public float m_spawnFrequency;

    public override void Initialize()
    {
        if (m_audioSource)
            m_audioSource.Play();
        m_boulders = GameObject.FindGameObjectsWithTag("Boulder");
    }

    public override void Uninitialize()
    {
        //called when this script is removed from the player
    }

    void Update () {
        m_timer += Time.deltaTime;
        if (m_timer >= m_spawnFrequency)
        {
            foreach (GameObject boulder in m_boulders)
            {
                if (boulder != null)
                    Instantiate(m_fire, boulder.transform.position, Quaternion.identity);
            }
        }
	}
}
