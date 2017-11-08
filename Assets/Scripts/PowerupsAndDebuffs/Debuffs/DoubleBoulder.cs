using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBoulder : PowerupOrDebuff {

    public GameObject m_boulder;

    private GameObject[] m_oldBoulders;
    private List<GameObject> m_newBoulders = new List<GameObject>();

    public override void Initialize()
    {
        // create boulder and adjust sizes
        m_oldBoulders = GameObject.FindGameObjectsWithTag("Boulder");
        foreach (GameObject boulder in m_oldBoulders)
        {
            m_newBoulders.Add(Instantiate(m_boulder, boulder.transform.position, boulder.transform.rotation));
        }
    }

    public override void Uninitialize()
    {
        for (int i = m_newBoulders.Count - 1; i >= 0; i --)
        {
            Destroy(m_newBoulders[i].gameObject);
        }
    }

}
