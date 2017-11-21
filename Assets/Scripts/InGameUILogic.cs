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
    public Text m_gemCounter;
    

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");

        m_gc = go.GetComponent<GameController>();
        m_canvas = GetComponent<Canvas>();
        m_gemCounter.text = "test";
    }
	
	// Update is called once per frame
	void Update () {

        int tp = (int)m_gc.m_secondsPassed;

        if(tp >=60) {
            m_minutes = tp / 60;
            m_seconds = (tp % 60 >= 0)? tp % 60 : 0;
            m_gemCounter.text = m_minutes.ToString() + ":" + m_seconds.ToString() ;
        }else{
            m_seconds = tp;
            m_gemCounter.text = "0:" + m_seconds.ToString();
        }
    }
}
