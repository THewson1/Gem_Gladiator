using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilestoneLogic : MonoBehaviour {

    public Transform[] m_spawnLocations;
    public GameObject m_objectToSpawn;
    public int m_numberOfGemsToReach;
    public bool m_repeating;

    private int m_lastMilestoneHit;
	
	// Update is called once per frame
	void Update () {
        GameController gT = GetComponent<GameController>();
        if (!m_repeating && gT.m_amountOfGems > m_numberOfGemsToReach)
        {
            foreach (Transform location in m_spawnLocations)
            {
                Instantiate(m_objectToSpawn, location);
            }
            return;
        }

        if (m_repeating && gT.m_amountOfGems - m_lastMilestoneHit > m_numberOfGemsToReach)
        {
            m_lastMilestoneHit = gT.m_amountOfGems;
            foreach (Transform location in m_spawnLocations)
            {
                Instantiate(m_objectToSpawn, location);
            }
        }
	}
}
