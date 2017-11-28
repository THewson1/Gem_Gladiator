using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterNameUI : MonoBehaviour {

    public InputField m_inputField;
    public Text m_score;
    public GameObject m_highscoreUI;

    private GameObject m_gc;
    private int m_finalScore = 0;

    private void Start()
    {
        m_gc = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnEnable()
    {
        Debug.Log("enabled");
        m_finalScore = m_gc.GetComponent<EndGameCondition>().m_finalScore;
        m_score.text = m_finalScore.ToString();
        Debug.Log(m_finalScore);
    }
	
    public void ApplyHighScore()
    {
        Debug.Log("apply");
        m_highscoreUI.GetComponent<HighScoreUI>().AddHighScore(m_inputField.text, m_finalScore);
        m_highscoreUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
