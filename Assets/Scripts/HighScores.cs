﻿using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

[System.Serializable]
public class HighScores {

    // the properties will be serialized
    public List<string> m_players = new List<string>();
    public List<int> m_scores = new List<int>();

    public void Save(string fileLocation)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileLocation);
        if (File.Exists(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(this);
            File.WriteAllText(filePath, dataAsJson);
        }
        else
        {
            Debug.Log("creating file located at " + filePath);
            File.Create(filePath).Close();
        }
    }

    public HighScores Load(string fileLocation){
        string filePath = Path.Combine(Application.streamingAssetsPath, fileLocation);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            HighScores loadedData = JsonUtility.FromJson<HighScores>(dataAsJson);
            if (loadedData == null)
            {
                return FixCorruptHighScores(fileLocation, dataAsJson);
            }
            return loadedData;
        }
        else
        {
            Debug.Log("creating file located at " + filePath);
            File.Create(filePath).Close();
            HighScores newData = new HighScores();
            return newData;
        }
    }

    HighScores FixCorruptHighScores(string fileLocation, string corruptData)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Corrupt HighScores.json");
        if (File.Exists(filePath))
        {
            Debug.Log("creating file located at " + filePath);
            File.WriteAllText(filePath, corruptData);
        }
        else
        {
            File.Create(filePath).Close();
            FixCorruptHighScores(fileLocation, corruptData);
        }
        return new HighScores();
    }

    public void AddHighScore(string name, int score){
        m_players.Add(name);
        m_scores.Add(score);
    }
}
