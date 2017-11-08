using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class EndGameCondition : MonoBehaviour {

    public bool m_singlePlayer;
    public bool m_coop;
    public bool m_versus;

    public int m_gameMode;
    private GameObject[] m_players;
	
    void Start ()
    {
        m_players = GameObject.FindGameObjectsWithTag("Player");
    }

	void Update () {
        bool gameOver = false;
		switch(m_gameMode)
        {
            //single player
            case (1):
                foreach (GameObject player in m_players)
                {
                    if (player.GetComponent<PlayerDeathLogic>().m_lives <= 0)
                        gameOver = true;
                }
            break;
            //co-op
            case (2):
                foreach (GameObject player in m_players)
                {
                    PlayerDeathLogic playerDeathLogic = player.GetComponent<PlayerDeathLogic>();
                    if (playerDeathLogic.m_lives <= 0)
                        gameOver = true;
                    if (playerDeathLogic.m_lives > 0)
                        gameOver = false;
                }
                break;
            //verses
            case (3):
                foreach (GameObject player in m_players)
                {
                    if (player.GetComponent<PlayerDeathLogic>().m_lives <= 0)
                        gameOver = true;
                }
                break;
            default:
                
            break;
        }

        if (gameOver)
        {
            //show game over screen
            Debug.Log("GameOver");
        }
	}
}

[CustomEditor(typeof(EndGameCondition))]
public class EndGameConditionEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as EndGameCondition;

        EditorGUILayout.TextArea("Please Only Pick One From Below");
        myScript.m_singlePlayer = EditorGUILayout.Toggle("Single Player", myScript.m_singlePlayer);
        myScript.m_coop = EditorGUILayout.Toggle("Co-op", myScript.m_coop);
        myScript.m_versus = EditorGUILayout.Toggle("Versus", myScript.m_versus);

    }
}

