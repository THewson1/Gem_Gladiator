using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyRamp : MonoBehaviour {

    public bool m_rampByMinimumSpeed;
    public bool m_rampByAggressiveness;

    public float m_rateOfSpeedIncrease;
    public float m_rateOfAgroIncrease;
	
	void Update () {
        // make a list of the boulders (this can change during gameplay so it must always be looked for in the current setup)
        GameObject[] boulders = GameObject.FindGameObjectsWithTag("Boulder");

        // add to the correct values for all the boulders
        for (int i = 0; i < boulders.Length; i++)
        {
            if (m_rampByAggressiveness)
            {
                SeekPlayer boulderLogic = boulders[i].GetComponent<SeekPlayer>();
                boulderLogic.seekSpeed += m_rateOfAgroIncrease;
            }
            if (m_rampByMinimumSpeed)
            {
                MaintainVelocity boulderLogic = boulders[i].GetComponent<MaintainVelocity>();
                boulderLogic.m_desiredVelocity += m_rateOfSpeedIncrease;
            }
        }
	}
}