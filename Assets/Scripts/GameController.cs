using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject m_playerPrefab;
    public GameObject m_boulderPrefab;
    [Range(1, 2)] public int m_amountOfPlayers = 1;

    public Vector3 m_playerSpawnOffset;
    public Vector3 m_boulderSpawnOffset;
    public int m_pointValueOf1Gem;
    public int m_pointValueOf1Second;

    private float m_secondsPassed;
    [HideInInspector]
    public Dictionary<int, int> m_amountOfGems = new Dictionary<int, int>();

    [HideInInspector] public List<GameObject> m_listOfPlayers = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        m_secondsPassed = 0;
        //create the correct amount of players at the start of the game

		for (int i = 0; i < m_amountOfPlayers; i++)
        {
            if (m_amountOfPlayers % 2 != 0)
            {
                if (i == (m_amountOfPlayers-1)/2)
                {
                    CreatePlayer(0);
                }
            }
            else
            {
                CreatePlayer(i);
            }
        }

        Invoke("SpawnBoulderAfterFalling", 8);
    }

    void SpawnBoulderAfterFalling()
    {
        CreateBoulder();
        AddPlayersToBoulderTargets();
        for (int i = 0; i < m_listOfPlayers.Count; i ++)
        {
            m_listOfPlayers[i].GetComponent<PlayerInput>().enabled = true;
        }
    }

    private void CreateBoulder()
    {
        GameObject newBoulder = Instantiate(m_boulderPrefab, m_playerSpawnOffset, Quaternion.identity);
    }

    private void CreatePlayer(int i)
    {
        GameObject newPlayer = Instantiate(m_playerPrefab, new Vector3(i - m_amountOfPlayers / 2, 1, 0) + m_playerSpawnOffset, Quaternion.identity);
        m_listOfPlayers.Add(newPlayer);
        PlayerInput pi = newPlayer.GetComponent<PlayerInput>();
        pi.PlayerNumber = i;
        pi.enabled = false;
        m_amountOfGems.Add(i, 0);
    }

    private void AddPlayersToBoulderTargets()
    {
        BounceInternaly boulderBouncingScript = GameObject.FindGameObjectWithTag("Boulder").GetComponent<BounceInternaly>();
        foreach (GameObject player in m_listOfPlayers)
            boulderBouncingScript.m_objectsToBounceTowards.Add(player);
    }

    private void Update()
    {
        if (!GetComponent<EndGameCondition>().m_gameOver)
            m_secondsPassed += Time.deltaTime;
    }


    public int CalculateFinalScore(GameObject[] players)
    {
        int finalScore = 0;
        foreach (GameObject player in players)
        {
            finalScore += (m_amountOfGems[player.GetComponent<PlayerInput>().m_playerNumber] * m_pointValueOf1Gem);
        }
        finalScore += ((int)m_secondsPassed * m_pointValueOf1Second);
        return finalScore;
    }
}
