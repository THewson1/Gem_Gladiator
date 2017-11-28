﻿using System.Collections;
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
    public int m_amountOfGemsRequiredToWin = 0;
    public GameObject m_gameOverScreen;
    [HideInInspector]
    public bool m_gameOver = false;

    private int m_gameMode;
    private GameObject[] m_players;
    [HideInInspector]
    public int m_finalScore = 0;
	
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
        if (!m_gameOver)
        { 
		    switch(m_gameMode)
            {
                //single player
                case (1):
                    foreach (GameObject player in m_players)
                    {
                        if (player.GetComponent<PlayerDeathLogic>().m_lives <= 0)
                            m_gameOver = true;
                    }
                break;
                //co-op
                case (2):
                    m_gameOver = true;
                    foreach (GameObject player in m_players)
                    {
                        PlayerDeathLogic playerDeathLogic = player.GetComponent<PlayerDeathLogic>();
                        if (playerDeathLogic.m_lives > 0)
                        {
                            m_gameOver = false;
                            break;
                        }
                    }
                    break;
                //verses
                case (3):
                    foreach (GameObject player in m_players)
                    {
                        PlayerDeathLogic playerLogic;
                        if (playerLogic = player.GetComponent<PlayerDeathLogic>())
                        {
                            int playerNumber = player.GetComponent<PlayerInput>().m_playerNumber;
                            if (playerLogic.m_lives <= 0)
                                m_gameOver = true;
                            if (m_amountOfGemsRequiredToWin != 0 && GetComponent<GameController>().m_amountOfGems[playerNumber] > m_amountOfGemsRequiredToWin)
                                m_gameOver = true;
                        }
                    }
                    break;
                default:
                    //default
                break;
            }

            if (m_gameOver)
            {
                //show game over screen (display game over GUI)
                Debug.Log("GameOver");
                Invoke("GameOver", 5);
            }
        }
	}

    void GameOver() {
        GameController gC = GetComponent<GameController>();
        switch (m_gameMode)
        {
            case (0):
                m_finalScore = gC.CalculateFinalScore(m_players);
                m_gameOverScreen.SetActive(true);
       //         Time.timeScale = 0;
                break;

            case (1):
                m_finalScore = gC.CalculateFinalScore(m_players);
                m_gameOverScreen.SetActive(true);
        //        Time.timeScale = 0;
                break;

            case (2):
                foreach (GameObject player in m_players)
                {
                    if (player.GetComponent<PlayerDeathLogic>().m_lives > 0)
                    {
                        // whoever is not dead has won
                        m_gameOverScreen.SetActive(true);
        //                Time.timeScale = 0;
                    }
                }
                break;
        }
    }
}