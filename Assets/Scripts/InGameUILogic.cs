using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUILogic : MonoBehaviour {

    public short m_lifeCounter;
    //public bool m_dashBool;
    //public short m_dashCoolDown;
    public int m_seconds;
    public int m_minutes;
        
    GameController m_gc;
    Canvas m_canvas;
    public Text m_p1GemCounter;
    public Text m_p2GemCounter;
    public Image m_p1DashCooldownBar;
    public Image m_p2DashCooldownBar;
    public Image m_p1AttackCooldownBar;
    public Image m_p2AttackCooldownBar;
    public Text m_timer;
    

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");

        m_gc = go.GetComponent<GameController>();
        m_canvas = GetComponent<Canvas>();
       }
	
	// Update is called once per frame
	void Update () {

        // Time Displayer
        int tp = (int)m_gc.m_secondsPassed;

        if(tp >=60) {
            m_minutes = tp / 60;
            m_seconds = (tp % 60 >= 0)? tp % 60 : 0;
            m_timer.text = m_minutes.ToString() + ":" + ((m_seconds >= 10)? m_seconds.ToString() : "0" + m_seconds.ToString());
        }else{
            m_seconds = tp;
            m_timer.text = "0:" + ((m_seconds >= 10) ? m_seconds.ToString() : "0" + m_seconds.ToString());
        }

        // Gem Counter
        if (m_p1GemCounter)
            m_p1GemCounter.text = "Gems: " + m_gc.m_amountOfGems[0].ToString();
        if (m_p2GemCounter)
            m_p2GemCounter.text = "Gems: " + m_gc.m_amountOfGems[1].ToString();

        // DashCooldown & attackCooldown
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            PlayerInput playerInput = player.GetComponent<PlayerInput>();

            // Player 1 dash bar
            if (playerInput.m_playerNumber == 0)
            {
                float normalizedDodge = playerInput.m_dodging / playerInput.m_dodgeCooldown;
                m_p1DashCooldownBar.fillAmount = normalizedDodge;
            }

            // PLayer 2 dash bar
            if (playerInput.m_playerNumber == 1)
            {
                float normalizedDodge = playerInput.m_dodging / playerInput.m_dodgeCooldown;
                m_p2DashCooldownBar.fillAmount = normalizedDodge;
            }

            // Player 1 attack bar
            if (playerInput.m_playerNumber == 0)
            {
                float normalizedAttack = playerInput.m_attacking / playerInput.m_attackCooldown;
                m_p1AttackCooldownBar.fillAmount = normalizedAttack;
            }

            // PLayer 2 attack bar
            if (playerInput.m_playerNumber == 1)
            {
                float normalizedAttack = playerInput.m_attacking / playerInput.m_attackCooldown;
                m_p2AttackCooldownBar.fillAmount = normalizedAttack;
            }
        }

    }  
}
