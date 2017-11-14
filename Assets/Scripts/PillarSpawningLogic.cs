using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpawningLogic : MonoBehaviour {

    public float m_initialDelay;
    public float m_respawnTimer;
    public GameObject m_pillarPrefab;
    public Transform[] m_pillarLocations;

	// Use this for initialization
	void Start () {
        Invoke("StartSpawningPillars", m_initialDelay);
	}
	
    void StartSpawningPillars()
    {
        foreach(Transform pillarLoaction in m_pillarLocations)
        {
            if (pillarLoaction.childCount == 0)
                SpawnPillar(pillarLoaction);
        }
        Invoke("StartSpawningPillars", m_respawnTimer);
    }

    void SpawnPillar(Transform loaction)
    {
        Instantiate(m_pillarPrefab, loaction);
    }

}
