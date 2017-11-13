using System.Collections;
using System.Collections.Generic;
//#if UNITY_EDITOR
using UnityEditor;
//#endif
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EndGameCondition : MonoBehaviour {

    public bool m_singlePlayer;
    public bool m_coop;
    public bool m_versus;

    private int m_gameMode;
    private GameObject[] m_players;
	
    void Start ()
    {
        if (m_singlePlayer)
            m_gameMode = 1;
        if (m_coop)
            m_gameMode = 2;
        if (m_versus)
            m_gameMode = 3;
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
                gameOver = true;
                foreach (GameObject player in m_players)
                {
                    PlayerDeathLogic playerDeathLogic = player.GetComponent<PlayerDeathLogic>();
                    if (playerDeathLogic.m_lives > 0)
                    {
                        gameOver = false;
                        break;
                    }
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
            //show game over screen (display game over GUI)
            Debug.Log("GameOver");
            SceneManager.LoadScene(0); // this is temp
        }
	}
}