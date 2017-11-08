using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMagnet : PowerupOrDebuff{

    private List<GameObject> m_listOfGems = new List<GameObject>();
    public float m_speed = 10000f;

    public override void Initialize()
    {
        //called when this script is applied to the player
    }

    public override void Uninitialize()
    {
        //called when this script is removed from the player
    }

    void Update() {
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");

        m_listOfGems.Clear();
        m_listOfGems.AddRange(gems);
    }

    void FixedUpdate () {
        for (int i = 0; i < m_listOfGems.Count; i ++)
        {
            if (m_listOfGems[i] != null)
            {
                Vector3 directionTowardsPlayer = (transform.position - m_listOfGems[i].transform.position);
                m_listOfGems[i].GetComponent<Rigidbody>().AddForce(directionTowardsPlayer.normalized * m_speed * Time.deltaTime);
            }
        }
    }
}
