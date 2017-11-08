using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject m_playerPrefab;
    [Range(1, 4)] public int m_amountOfPlayers = 1;

    public Vector3 m_playerSpawnOffset;

    public int m_amountOfGems;
    [HideInInspector] public List<GameObject> m_listOfPlayers = new List<GameObject>();

	// Use this for initialization
	void Start () {
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
        AddPlayersToBoulderTargets();
    }

    private void CreatePlayer(int i)
    {
        GameObject newPlayer = Instantiate(m_playerPrefab, new Vector3(i - m_amountOfPlayers / 2, 1, 0) + m_playerSpawnOffset, Quaternion.identity);
        m_listOfPlayers.Add(newPlayer);
        newPlayer.GetComponent<PlayerInput>().PlayerNumber = i;
    }

    private void AddPlayersToBoulderTargets()
    {
        BounceInternaly boulderBouncingScript = GameObject.FindGameObjectWithTag("Boulder").GetComponent<BounceInternaly>();
        foreach (GameObject player in m_listOfPlayers)
            boulderBouncingScript.m_objectsToBounceTowards.Add(player);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
