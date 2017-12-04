using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePowerupAfterAmountOfGems : MonoBehaviour {

    public GameObject[] m_powerups;
    public int m_requiredAmountOfGems;

    private GameObject m_gameController;

    private void Start()
    {
        m_gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update () {
        GameController gc = m_gameController.GetComponent<GameController>();

        // for all the players
        for (int i = 0; i < gc.m_listOfPlayers.Count; i++)
        {
            int playerToGetPowerup = -1;
            int pn = gc.m_listOfPlayers[i].GetComponent<PlayerInput>().m_playerNumber;

            // if a player has enough gems to get a powerup
            if (gc.m_amountOfGems[pn] >= m_requiredAmountOfGems)
            {
                playerToGetPowerup = pn;
                gc.m_amountOfGems[pn] = 0;
            }

            // if a player is tagged to get a powerup
            if (playerToGetPowerup != -1)
            {
                // find that player and give them a random powerup
                for (int j = 0; j < gc.m_listOfPlayers.Count; j++)
                {
                    if (gc.m_listOfPlayers[j].GetComponent<PlayerInput>().m_playerNumber == pn)
                    {
                        AddRandomPowerup(gc.m_listOfPlayers[j]);
                    }
                }
            }
        }
	}

    void AddRandomPowerup(GameObject player)
    {
        // pick a random powerup and give it to the player
        int powerup = Random.Range(0, m_powerups.Length);
        m_powerups[powerup].GetComponent<PowerupAndDebuffBehaviour>().CopyPowerupOrDebuffToPlayer(player);
    }
}
