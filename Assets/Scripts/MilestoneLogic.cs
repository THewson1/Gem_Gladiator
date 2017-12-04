using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilestoneLogic : MonoBehaviour {

    public Transform[] m_spawnLocations;
    public GameObject m_objectToSpawn;
    public bool m_destroyObjectAfterSpawning;
    public float m_objectLifetime = 5;
    public int m_numberOfGemsToReach;
    public bool m_repeating;

    private int m_lastMilestoneHit;

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        GameController gc = null;
        if (go != null)
        {
            gc = go.GetComponent<GameController>();
            int amountOfGems = 0;
            foreach (KeyValuePair<int, int> player in gc.m_amountOfGems)
            {
                amountOfGems += player.Value;
            }

            // If the number of gems to reach is reached spawn the Milestone object
            if (!m_repeating && amountOfGems >= m_numberOfGemsToReach)
            {
                foreach (Transform location in m_spawnLocations)
                {
                    SpawnObjects(location);
                }
                return;
            }

            // If the number of gems to reach is reached spawn the Milestone object (repeating)
            if (m_repeating && amountOfGems - m_lastMilestoneHit >= m_numberOfGemsToReach)
            {
                m_lastMilestoneHit = amountOfGems;
                foreach (Transform location in m_spawnLocations)
                {
                    SpawnObjects(location);
                }
            }
        }
    }

    /// <summary>
    /// Spawn milestone object
    /// </summary>
    void SpawnObjects(Transform location)
    {
        GameObject objectToSpawn = Instantiate(m_objectToSpawn, location);
        if (m_destroyObjectAfterSpawning)
            objectToSpawn.AddComponent<DestroyAfterTime>().m_lifeTime = m_objectLifetime;
    }
}
