using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePowerupAfterAmountOfGems : MonoBehaviour {

    public GameObject[] m_powerups;
    public int m_requiredAmountOfGems;
	
	// Update is called once per frame
	void Update () {
        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        int playerToGetPowerup = -1;
        foreach(KeyValuePair<int, int> playernumber in gameController.m_amountOfGems)
        {
            if (playernumber.Value >= m_requiredAmountOfGems)
            {
                playerToGetPowerup = playernumber.Key;
                gameController.m_amountOfGems[playernumber.Key] = 0;
            }
        }
        if (playerToGetPowerup != -1)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<PlayerInput>().m_playerNumber == playerToGetPowerup)
                {
                    AddRandomPowerup(players[i]);
                }
            }
        }
	}

    void AddRandomPowerup(GameObject player)
    {
        int powerup = Random.Range(0, m_powerups.Length);
        m_powerups[powerup].GetComponent<PowerupAndDebuffBehaviour>().CopyPowerupOrDebuffToPlayer(player);
    }
}
