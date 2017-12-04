using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUILogic : MonoBehaviour
{

    public bool m_activatePlayer1;
    public bool m_activatePlayer2;

    private int m_seconds;
    private int m_minutes;

    GameController m_gc;
    Canvas m_canvas;
    public Text m_p1GemCounter;
    public Text m_p2GemCounter;
    public Image m_p1DashCooldownBar;
    public Image m_p2DashCooldownBar;
    public Image m_p1AttackCooldownBar;
    public Image m_p2AttackCooldownBar;
    public Text m_timer;
    public List<Image> m_p1Modifiers;
    public List<Image> m_p2Modifiers;

    public List<Image> m_player1Lives;
    public List<Image> m_player2Lives;

    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");

        m_gc = go.GetComponent<GameController>();
        m_canvas = GetComponent<Canvas>();

        //enable the correct life images for player 1
        if (m_activatePlayer1)
        {
            for (int i = 0; i != m_player1Lives.Count; i++)
            {
                m_player1Lives[i].enabled = false;
            }
        }

        //enable the correct life images for player 2
        if (m_activatePlayer2)
        {
            for (int i = 0; i != m_player2Lives.Count; i++)
            {
                m_player2Lives[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Life Counter
        if  (m_activatePlayer1){
            for (int i = 0; i!= 2; i++) {
                m_player1Lives[i].enabled = false;
            }

            PlayerDeathLogic lives =  m_gc.m_listOfPlayers[0].GetComponent<PlayerDeathLogic>();
            for(int i = 0; i < lives.m_lives - 1; i++) {
                m_player1Lives[i].enabled = true;
            }           
        }
        if(m_activatePlayer2) {
            for(int i = 0; i != 2; i++) {
                m_player2Lives[i].enabled = false;
            }

            PlayerDeathLogic lives = m_gc.m_listOfPlayers[1].GetComponent<PlayerDeathLogic>();
            for(int i = 0; i < lives.m_lives -1; i++) {
                m_player2Lives[i].enabled = true;
            }
        }

        // Time Displayer
        int tp = (int)m_gc.m_secondsPassed;

        if (tp >= 60)
        {
            m_minutes = tp / 60;
            m_seconds = (tp % 60 >= 0) ? tp % 60 : 0;
            m_timer.text = m_minutes.ToString() + ":" + ((m_seconds >= 10) ? m_seconds.ToString() : "0" + m_seconds.ToString());
        }
        else
        {
            m_seconds = tp;
            m_timer.text = "0:" + ((m_seconds >= 10) ? m_seconds.ToString() : "0" + m_seconds.ToString());
        }

        // Gem Counter
        if (m_activatePlayer1)
            if (m_p1GemCounter)
                m_p1GemCounter.text = m_gc.m_amountOfGems[0].ToString();
        if (m_activatePlayer2)
        {
            if (m_p2GemCounter)
                m_p2GemCounter.text = m_gc.m_amountOfGems[1].ToString();
        }

        // DashCooldown & attackCooldown
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerInput playerInput = player.GetComponent<PlayerInput>();

            if (m_activatePlayer1)
            {
                // Player 1 dash bar
                if (playerInput.m_playerNumber == 0)
                {
                    float normalizedDodge = playerInput.m_dodging / playerInput.m_dodgeCooldown;
                    m_p1DashCooldownBar.fillAmount = normalizedDodge;
                }

                // Player 1 attack bar
                if (playerInput.m_playerNumber == 0)
                {
                    float normalizedAttack = playerInput.m_attacking / playerInput.m_attackCooldown;
                    m_p1AttackCooldownBar.fillAmount = normalizedAttack;
                }
            }

            if (m_activatePlayer2)
            {
                // PLayer 2 dash bar
                if (playerInput.m_playerNumber == 1)
                {
                    float normalizedDodge = playerInput.m_dodging / playerInput.m_dodgeCooldown;
                    m_p2DashCooldownBar.fillAmount = normalizedDodge;
                }

                // PLayer 2 attack bar
                if (playerInput.m_playerNumber == 1)
                {
                    float normalizedAttack = playerInput.m_attacking / playerInput.m_attackCooldown;
                    m_p2AttackCooldownBar.fillAmount = normalizedAttack;
                }
            }
        }

        UpdateModifiers();

    }

    void UpdateModifiers()
    {
        GameObject thisPlayer = null;
        PowerupOrDebuff[] modifiers;

        if (m_activatePlayer1)
        {
            // get player 1
            foreach (GameObject player in m_gc.m_listOfPlayers)
            {
                if (player.GetComponent<PlayerInput>().m_playerNumber == 0)
                    thisPlayer = player;
            }
            // loop through all modifier images on player 1
            modifiers = thisPlayer.GetComponents<PowerupOrDebuff>();
            for (int i = 0; i < m_p1Modifiers.Count; i ++)
            {
                // if there is still a modifier on th eplayer to display the image for then display it
                if (i < modifiers.Length)
                {
                    m_p1Modifiers[i].enabled = true;
                    m_p1Modifiers[i].sprite = modifiers[i].m_image;
                }
                // if not hide the current modifier image
                else
                {
                    m_p1Modifiers[i].enabled = false;
                }
            }
        }
        if (m_activatePlayer2)
        {
            // get player 1
            foreach (GameObject player in m_gc.m_listOfPlayers)
            {
                if (player.GetComponent<PlayerInput>().m_playerNumber == 1)
                    thisPlayer = player;
            }
            // loop through all modifier images on player 1
            modifiers = thisPlayer.GetComponents<PowerupOrDebuff>();
            for (int i = 0; i < m_p2Modifiers.Count; i++)
            {
                // if there is still a modifier on th eplayer to display the image for then display it
                if (i < modifiers.Length)
                {
                    m_p2Modifiers[i].enabled = true;
                    m_p2Modifiers[i].sprite = modifiers[i].m_image;
                }
                // if not hide the current modifier image
                else
                {
                    m_p2Modifiers[i].enabled = false;
                }
            }
        }
    }
}