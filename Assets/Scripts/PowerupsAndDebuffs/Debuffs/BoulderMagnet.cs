using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMagnet : PowerupOrDebuff {

    private List<GameObject> m_listOfBoulders = new List<GameObject>();
    public float m_speed = 100000f;

    public override void Initialize()
    {
        //called when this script is applied to the player
    }

    public override void Uninitialize()
    {
        //called when this script is removed from the player
    }

    void Update()
    {
        GameObject[] boulders = GameObject.FindGameObjectsWithTag("Boulder");

        m_listOfBoulders.Clear();
        m_listOfBoulders.AddRange(boulders);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < m_listOfBoulders.Count; i++)
        {
            Vector3 directionTowardsPlayer = (transform.position - m_listOfBoulders[i].transform.position);
            m_listOfBoulders[i].GetComponent<Rigidbody>().AddForce(directionTowardsPlayer.normalized * m_speed * Time.deltaTime);
        }
    }
}
