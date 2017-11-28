using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour {

    HighScores m_highScores = new HighScores();

    [SerializeField]
    private List<Text> m_scoreTextFields;
    [SerializeField]
    private string highScoresFileLocation = "HighScores";

    public void AddHighScore(string name, int score)
    {
        m_highScores = m_highScores.Load(highScoresFileLocation + ".json");
        m_highScores.AddHighScore(name, score);
        m_highScores.Save(highScoresFileLocation + ".json");
    }

    void OnEnable()
    {
        m_highScores = m_highScores.Load(highScoresFileLocation + ".json");
        SortAndDisplayHighScores();
    }

    void SortAndDisplayHighScores()
    {
        // sort
        List<KeyValuePair<string, int>> highScoresToSort = new List<KeyValuePair<string, int>>();
        for (int i = 0; i < m_highScores.m_scores.Count; i++)
        {
            highScoresToSort.Add(new KeyValuePair<string, int>(m_highScores.m_players[i], m_highScores.m_scores[i]));
        }
        highScoresToSort.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
        m_highScores.m_scores.Clear();
        m_highScores.m_players.Clear();
        for (int i = 0; i < highScoresToSort.Count; i++)
        {
            m_highScores.m_players.Add(highScoresToSort[i].Key);
            m_highScores.m_scores.Add(highScoresToSort[i].Value);
        }

        // display
        for (int i = 0; i < m_scoreTextFields.Count; i++)
        {
            if (m_highScores.m_scores.Count > i)
                m_scoreTextFields[i].text = m_highScores.m_players[i] + " : " + m_highScores.m_scores[i];
            else
                m_scoreTextFields[i].text = "XXX : XXX";
        }
    }
}
