using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class DifficultyRamp : MonoBehaviour {

    public bool m_rampByMinimumSpeed;
    public bool m_rampByAggressiveness;

    public float m_rateOfSpeedIncrease;
    public float m_rateOfAgroIncrease;
	
	void Update () {
        GameObject[] boulders = GameObject.FindGameObjectsWithTag("Boulder");

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
/*
[CustomEditor(typeof(DifficultyRamp))]
public class DifficultyRampEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as DifficultyRamp;

        myScript.m_rampByMinimumSpeed = GUILayout.Toggle(myScript.m_rampByMinimumSpeed, "Ramp By Minimum Speed");

        if (myScript.m_rampByMinimumSpeed)
        {
            myScript.m_rateOfSpeedIncrease = EditorGUILayout.FloatField("Rate Of Increase", myScript.m_rateOfSpeedIncrease);
        }
        else
        {
            myScript.m_rateOfSpeedIncrease = 0;
        }

        myScript.m_rampByAggressiveness = GUILayout.Toggle(myScript.m_rampByAggressiveness, "Ramp By Aggressiveness");

        if (myScript.m_rampByAggressiveness)
        {
            myScript.m_rateOfAgroIncrease = EditorGUILayout.FloatField("Rate Of Increase", myScript.m_rateOfAgroIncrease);
        }
        else
        {
            myScript.m_rateOfAgroIncrease = 0;
        }

    }
}
*/