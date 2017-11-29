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

        int playerToGetPowerup = -1;
        int pn = -1;
        for (int i = 0; i < gc.m_listOfPlayers.Count; i++)
        {
            pn = gc.m_listOfPlayers[i].GetComponent<PlayerInput>().m_playerNumber;
            Debug.Log(gc.m_amountOfGems[pn] + " : PLayer" + pn);
            if (gc.m_amountOfGems[pn] >= m_requiredAmountOfGems)
            {
                playerToGetPowerup = pn;
                gc.m_amountOfGems[pn] = 0;
            }
            else
            {
                pn = -1;
            }
        }
        if (playerToGetPowerup != -1)
        {
            for (int i = 0; i < gc.m_listOfPlayers.Count; i ++)
            {
                if (gc.m_listOfPlayers[i].GetComponent<PlayerInput>().m_playerNumber == pn)
                {
                    AddRandomPowerup(gc.m_listOfPlayers[i]);
                    Debug.Log(gc.m_listOfPlayers[i]);
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
