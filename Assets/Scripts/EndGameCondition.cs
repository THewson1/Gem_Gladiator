using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameCondition : MonoBehaviour {
    public Text m_coopWinText;
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
        // set the gamemode variable to reflect the game mode selected
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
                    // if the player is dead the game is over
                    foreach (GameObject player in m_players)
                    {
                        if (player.GetComponent<PlayerDeathLogic>().m_lives <= 0)
                            m_gameOver = true;
                    }
                break;
                //co-op
                case (2):
                    m_gameOver = true;
                    // if at least one player has a life left the game is not over
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
                    // if at least one player is dead the game is over and the oposite player wins
                    foreach (GameObject player in m_players)
                    {
                        PlayerDeathLogic playerLogic;
                        if (playerLogic = player.GetComponent<PlayerDeathLogic>())
                        {
                            int playerNumber = player.GetComponent<PlayerInput>().m_playerNumber;
                            if (playerLogic.m_lives <= 0) {
                                if (playerNumber == 0)
                                {
                                    MakePlayerInvincible(1);
                                    m_coopWinText.text = "Winner is player 2!";
                                }

                                if (playerNumber == 1)
                                {
                                    MakePlayerInvincible(0);
                                    m_coopWinText.text = "Winner is player 1!";
                                }

                                m_gameOver = true;
                            }
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

    /// <summary>
    /// For use when the game is over and the specified player should not die
    /// </summary>
    void MakePlayerInvincible(int playerNumber)
    {
        foreach (GameObject player in m_players)
        {
            if (player.GetComponent<PlayerInput>().m_playerNumber == playerNumber)
            {
                player.GetComponent<PlayerDeathLogic>().m_invincible = true;
                player.layer = LayerMask.NameToLayer("Ghost");
            }
        }
    }

    /// <summary>
    /// Execute the correct action for the current gamemode when the game is over
    /// </summary>
    void GameOver() {
        switch (m_gameMode)
        {
            // calculate the fianl score, go to the game over screen and pause the physics
            case (1):
                m_finalScore = CalculateFinalScore();
                Debug.Log(m_finalScore);
                m_gameOverScreen.SetActive(true);
                Time.timeScale = 0;
                break;

            // calculate the fianl score, go to the game over screen and pause the physics
            case (2):
                m_finalScore = CalculateFinalScore();
                Debug.Log(m_finalScore);
                m_gameOverScreen.SetActive(true);
                Time.timeScale = 0;
                break;

            // Go to the game over screen and pause the physics
            case (3):
                // whoever is not dead has won
                m_gameOverScreen.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }

    /// <summary>
    /// Use math to find the correct final score
    /// </summary>
    public int CalculateFinalScore()
    {
        GameController gC = GetComponent<GameController>();
        int finalScore = 0;
        foreach (GameObject player in m_players)
        {
            finalScore += ( gC.m_amountOfGems[player.GetComponent<PlayerInput>().m_playerNumber] * gC.m_pointValueOf1Gem);
        }
        finalScore += ((int)gC.m_secondsPassed * gC.m_pointValueOf1Second);
        return finalScore;
    }
}