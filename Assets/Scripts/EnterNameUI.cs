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

    private void OnEnable()
    {
        m_gc = GameObject.FindGameObjectWithTag("GameController");
        m_finalScore = m_gc.GetComponent<EndGameCondition>().m_finalScore;
        m_score.text = m_finalScore.ToString();
    }
	
    /// <summary>
    /// Add the current highscore to the highscores file
    /// </summary>
    public void ApplyHighScore()
    {
        // if the name is not blank, apply the highscore
        if (m_inputField.text != "")
            m_highscoreUI.GetComponent<HighScoreUI>().AddHighScore(m_inputField.text, m_finalScore);
        m_highscoreUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
